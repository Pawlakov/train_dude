// <copyright file="ProjectionStoreFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Infrastructure.Tests;

using System;
using System.Threading.Tasks;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.Configuration;

using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.ReadModels;
using TrainDude.Features.Trips.Domain;
using TrainDude.Infrastructure.Lines.Projections;
using TrainDude.Infrastructure.Lines.ReadModels;
using TrainDude.Infrastructure.Radii.Projections;
using TrainDude.Infrastructure.Segments.Projections;
using TrainDude.Infrastructure.Settings.Projections;
using TrainDude.Infrastructure.Stations.Projections;
using TrainDude.Infrastructure.Trips.Projections;

using TUnit.Core.Interfaces;

public class ProjectionStoreFixture
    : IAsyncInitializer, IAsyncDisposable
{
    public IDocumentStore Store { get; private set; } = null!;

    public IProjectionDaemon Daemon { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets("0d6c2b99-3ac6-4582-b9c1-04c3013a5c88")
            .Build();

        this.Store = DocumentStore.For(options =>
        {
            options.Connection(config.GetConnectionString("Write"));
            options.DatabaseSchemaName = "train_dude_test";

            options.Projections.Add<SharedSettingsReferenceProjection>(ProjectionLifecycle.Inline);

            options.Projections.Add<LineAggregateProjection>(ProjectionLifecycle.Inline);
            options.Projections.Add<LineReadModelProjection>(ProjectionLifecycle.Async);
            options.Projections.Add<LineSegmentReferenceProjection>(ProjectionLifecycle.Inline);
            options.Projections.Add<LineTripReferenceProjection>(ProjectionLifecycle.Inline);
            options.Projections.Add<LineTripLinkProjection>(ProjectionLifecycle.Inline);

            options.Projections.Add<RadiusAggregateProjection>(ProjectionLifecycle.Inline);

            options.Projections.Add<SegmentAggregateProjection>(ProjectionLifecycle.Inline);
            options.Projections.Add<SegmentReadModelProjection>(ProjectionLifecycle.Async);
            options.Projections.Add<SegmentStationReferenceProjection>(ProjectionLifecycle.Inline);

            options.Projections.Add<StationAggregateProjection>(ProjectionLifecycle.Inline);
            options.Projections.Add<StationReadModelProjection>(ProjectionLifecycle.Async);

            options.Projections.Add<TripAggregateProjection>(ProjectionLifecycle.Inline);

            options.AutoCreateSchemaObjects = AutoCreate.All;
        });

        this.Daemon = await this.Store.BuildProjectionDaemonAsync();
    }

    public async ValueTask ResetAsync()
    {
        await this.Store.Advanced.Clean.DeleteAllEventDataAsync();

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(TripAggregate));

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(StationReadModel));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(StationAggregate));

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(SegmentStationReference));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(SegmentReadModel));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(SegmentAggregate));

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(RadiusAggregate));

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(LineTripLink));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(LineTripReference));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(LineSegmentReference));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(LineReadModel));
        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(LineAggregate));

        await this.Store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(SharedSettingsReference));
    }

    public ValueTask DisposeAsync()
    {
        this.Daemon.Dispose();
        this.Store.Dispose();
        return ValueTask.CompletedTask;
    }
}