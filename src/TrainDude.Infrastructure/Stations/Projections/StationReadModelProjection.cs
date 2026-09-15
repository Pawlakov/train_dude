// <copyright file="StationReadModelProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Stations.Projections;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Projections;

using TrainDude.Features.Settings;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;
using TrainDude.Infrastructure.Shared.ReadModels;
using TrainDude.Infrastructure.Stations.Events;
using TrainDude.Infrastructure.Stations.Groupers;

public sealed class StationReadModelProjection
    : MultiStreamProjection<StationReadModel, Guid>
{
    public StationReadModelProjection()
    {
        this.TransformsEvent<IStationEvent>();
        this.CustomGrouping(new StationReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<StationReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        var settings = await querySession.LoadAsync<SharedSettingsReference>(SettingsAccessor.SingletonId, cancellation);
        var policy = settings?.NamingPolicy ?? NamingPolicy.Modern;
        var nameSelector = NameResolver.GetNameSelector(policy);

        foreach (var slice in group.Slices)
        {
            var createdEvent = slice.Events().OfType<IEvent<StationCreated>>().SingleOrDefault();
            if (createdEvent is not null)
            {
                var name = nameSelector(createdEvent.Data);
                var enriched = new StationCreatedWithReferences(createdEvent.Data, name);

                slice.ReplaceEvent(createdEvent, enriched);
            }
        }
    }

    public void Apply(StationCreatedWithReferences e, StationReadModel readModel)
    {
        readModel.Id = e.Event.StationId;
        readModel.AxleCount = 1;
        readModel.NameGerman = e.Event.NameGerman;
        readModel.NameGermanNew = e.Event.NameGermanNew;
        readModel.NamePolish = e.Event.NamePolish;
        readModel.NameRussian = e.Event.NameRussian;
        readModel.Name = e.Name;
    }

    public void Apply(StationLocationSet e, StationReadModel readModel)
    {
        readModel.Location = e.Location;
    }

    public void Apply(StationAxleAdded e, StationReadModel readModel)
    {
        readModel.AxleCount += 1;
    }

    public void Apply(SettingsNamingPolicySet e, StationReadModel readModel)
    {
        var selector = NameResolver.GetNameSelector(e.NamingPolicy);
        readModel.Name = selector(readModel);
    }
}