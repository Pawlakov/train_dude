// <copyright file="MapService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using TrainDude.Queries.Contracts.Base;
using TrainDude.Shared.Values;
using TrainDude.Web.Client.GeoJson;

public sealed class MapService
    : IAsyncDisposable
{
    private readonly IJSRuntime js;
    private IJSObjectReference? scriptModule;
    private Task? initialized;

    public MapService(IJSRuntime js)
    {
        this.js = js;
    }

    public IMapQueryResult? CurrentData { get; private set; }

    public Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        return this.initialized ??= this.InitializeCoreAsync(cancellationToken);
    }

    public async Task ShowAsync(IMapQueryResult data, CancellationToken cancellationToken = default)
    {
        if (this.initialized is not null)
        {
            await this.initialized;
        }

        if (this.scriptModule is null)
        {
            throw new InvalidOperationException("Map has not been initialized.");
        }

        if (ReferenceEquals(data, this.CurrentData))
        {
            return;
        }

        var featureCollection = BuildGeoJson(data);

        if (FeatureFlags.MapEnabled)
        {
            await this.scriptModule.InvokeVoidAsync("clearGeoJson", cancellationToken);
            await this.scriptModule.InvokeVoidAsync("addGeoJson", cancellationToken, featureCollection);
        }

        this.CurrentData = data;
    }

    public async ValueTask DisposeAsync()
    {
        if (this.scriptModule is not null)
        {
            try
            {
                await this.scriptModule.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }
    }

    private async Task InitializeCoreAsync(CancellationToken cancellationToken = default)
    {
        this.scriptModule = await this.js.InvokeAsync<IJSObjectReference>("import", cancellationToken, "./Components/Layout/BaseMapLayout.razor.js");
        if (this.scriptModule != null)
        {
            if (FeatureFlags.MapEnabled)
            {
                await this.scriptModule.InvokeVoidAsync("initMap", cancellationToken, "map", 54.218000, 21.725389, 12);
            }
        }
    }

    private static GeoJsonFeatureCollection BuildGeoJson(IMapQueryResult data)
    {
        var features = new List<GeoJsonFeature>(data.StationPoints.Count + data.SegmentLineStrings.Count);

        foreach (var station in data.StationPoints)
        {
            features.Add(new GeoJsonFeature("Feature", new GeoJsonPoint(new[] { station.Longitude, station.Latitude, })));
        }

        foreach (var segment in data.SegmentLineStrings)
        {
            var coordinates = segment
                .Select(point => new[] { point.Longitude, point.Latitude, })
                .ToArray();

            features.Add(new GeoJsonFeature("Feature", new GeoJsonLineString(coordinates)));
        }

        return new GeoJsonFeatureCollection("FeatureCollection", features);
    }
}