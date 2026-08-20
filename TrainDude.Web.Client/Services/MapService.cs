// <copyright file="MapService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using TrainDude.Queries.Requests.Base;

public sealed class MapService
    : IAsyncDisposable
{
    private readonly IJSRuntime js;
    private readonly TaskCompletionSource initialized = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private IJSObjectReference? scriptModule;

    public MapService(IJSRuntime js)
    {
        this.js = js;
    }

    public IMapQueryResult? CurrentData { get; private set; }

    public async Task InitializeAsync()
    {
        if (this.scriptModule is not null)
        {
            return;
        }

        this.scriptModule = await this.js.InvokeAsync<IJSObjectReference>("import", "./Components/Layout/BaseMapLayout.razor.js");
        if (this.scriptModule != null)
        {
            await this.scriptModule.InvokeVoidAsync("initMap", "map", 54.218000, 21.725389, 12);
        }

        this.initialized.SetResult();
    }

    public async Task ShowAsync(IMapQueryResult data)
    {
        await this.initialized.Task;
        if (this.scriptModule == null)
        {
            throw new InvalidOperationException("Map has not been initialized.");
        }

        if (data == this.CurrentData)
        {
            return;
        }

        var stationsGeoJson = new List<string>();
        var segmentsGeoJson = new List<string>();

        foreach (var station in data.StationPoints)
        {
            stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": [{station.Longitude.ToString(CultureInfo.InvariantCulture)},{station.Latitude.ToString(CultureInfo.InvariantCulture)}] }}");
        }

        foreach (var segment in data.SegmentLineStrings)
        {
            var line = string.Join(',', segment.Select(x => $"[{x.Longitude.ToString(CultureInfo.InvariantCulture)},{x.Latitude.ToString(CultureInfo.InvariantCulture)}]"));

            segmentsGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");
        }

        var geoJson = $"[{string.Join(',', segmentsGeoJson.Concat(stationsGeoJson))}]";

        await this.scriptModule.InvokeVoidAsync("clearGeoJson");
        await this.scriptModule.InvokeVoidAsync("addGeoJson", geoJson);

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
}