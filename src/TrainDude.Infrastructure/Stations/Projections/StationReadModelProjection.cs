// <copyright file="StationReadModelProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Stations.Projections;

using System;
using System.Threading;
using System.Threading.Tasks;

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
                var enriched = new StationCreatedWithReferences(e.Data.StationId, e.Data.Who, e.Data.NameGerman, e.Data.NameGermanNew, e.Data.NamePolish, e.Data.NameRussian, name);

                slice.ReplaceEvent(e, enriched);
            });
    }

    public void Apply(StationCreatedWithReferences e, StationReadModel readModel)
    {
        readModel.Id = e.StationId;
        readModel.AxleCount = 1;
        readModel.NameGerman = e.NameGerman;
        readModel.NameGermanNew = e.NameGermanNew;
        readModel.NamePolish = e.NamePolish;
        readModel.NameRussian = e.NameRussian;
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
        var selector = StationNameResolver.GetNameSelector(e.NamingPolicy);
        readModel.Name = selector(readModel);
    }
}