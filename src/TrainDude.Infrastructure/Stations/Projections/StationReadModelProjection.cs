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

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;
using TrainDude.Infrastructure.Stations.Events;
using TrainDude.Infrastructure.Stations.Groupers;

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
        await group
            .EnrichWith<SharedSettingsReference>()
            .ForEvent<StationCreated>()
            .ForEntityId(_ => SettingsSingleton.Id)
            .EnrichAsync((slice, e, settings) =>
            {
                var policy = settings?.NamingPolicy ?? NamingPolicy.Modern;
                var nameSelector = StationNameResolver.GetNameSelector(policy);

                var name = nameSelector(e.Data);
                var enriched = new StationCreatedWithReferences(e.Data, name);

                slice.ReplaceEvent(e, enriched);
            });
    }

    public void Apply(IEvent<StationCreatedWithReferences> e, StationReadModel readModel)
    {
        readModel.Id = e.Data.Event.Id;
        readModel.AxleCount = 1;
        readModel.NameGerman = e.Data.Event.NameGerman;
        readModel.NameGermanNew = e.Data.Event.NameGermanNew;
        readModel.NamePolish = e.Data.Event.NamePolish;
        readModel.NameRussian = e.Data.Event.NameRussian;
        readModel.Name = e.Data.Name;

        readModel.Version++;
    }

    public void Apply(IEvent<StationLocationSet> e, StationReadModel readModel)
    {
        readModel.Location = e.Data.Location;

        readModel.Version++;
    }

    public void Apply(IEvent<StationAxleAdded> e, StationReadModel readModel)
    {
        readModel.AxleCount += 1;

        readModel.Version++;
    }

    public void Apply(SettingsNamingPolicySet e, StationReadModel readModel)
    {
        var selector = StationNameResolver.GetNameSelector(e.NamingPolicy);
        readModel.Name = selector(readModel);
    }
}