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

using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Stations.Domain.Events;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.ReadModels;
using TrainDude.Features.Stations.ReadModels.Events;
using TrainDude.Infrastructure.Stations.Groupers;
using TrainDude.Shared.Enums;

public class StationReadModelProjection
    : MultiStreamProjection<StationReadModel, Guid>
{
    public StationReadModelProjection()
    {
        this.Identity<StationCreated>(e => e.Id);
        this.Identity<StationLocationSet>(e => e.Id);
        this.Identity<StationAxleAdded>(e => e.Id);

        this.CustomGrouping(new StationReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<StationReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        var createdEvents = group.Slices
            .SelectMany(slice => slice.Events().OfType<IEvent<StationCreated>>())
            .ToArray();

        if (createdEvents.Length == 0)
        {
            return;
        }

        var settings = await querySession.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id, cancellation);

        var policy = settings?.NamingPolicy ?? NamingPolicy.Modern;
        var nameSelector = StationNameResolver.GetNameSelector(policy);

        foreach (var slice in group.Slices)
        {
            foreach (var e in slice.Events().OfType<IEvent<StationCreated>>().ToArray())
            {
                var name = nameSelector(e.Data);
                var enriched = new StationCreatedWithReferences(e.Data.Id, e.Data.When, name);

                slice.ReplaceEvent(e, enriched);
            }
        }
    }

    public void Apply(IEvent<StationCreatedWithReferences> e, StationReadModel readModel)
    {
        readModel.Id = e.Data.Id;
        readModel.Name = e.Data.Name;

        this.Version++;
    }

    public void Apply(IEvent<StationLocationSet> e, StationReadModel readModel)
    {
        readModel.Location = e.Data.Location;

        this.Version++;
    }

    public void Apply(IEvent<StationAxleAdded> e, StationReadModel readModel)
    {
        readModel.AxleCount += 1;

        this.Version++;
    }
}