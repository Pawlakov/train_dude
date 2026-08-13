// <copyright file="NetworkMap.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Web.Client.Features.Network;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using TrainDude.Queries.Requests.Network;

public partial class NetworkMap
    : ComponentBase
{
    private IJSObjectReference scriptModule;
    private string? geoJson;
    private bool dataPresent;
    private bool loadingActive;

    protected override async Task OnParametersSetAsync()
    {
        this.loadingActive = true;
        this.StateHasChanged();

        var result = await this.Mediator.Send(new GetNetworkQuery());
        this.geoJson = this.BuildGeoJson(result);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this.scriptModule = await this.JS.InvokeAsync<IJSObjectReference>("import", "./Components/Layout/BaseMapLayout.razor.js");
        }

        if (!dataPresent && this.geoJson != null)
        {
            await this.scriptModule.InvokeVoidAsync("clearGeoJson");
            await this.scriptModule.InvokeVoidAsync("addGeoJson", this.geoJson);

            this.dataPresent = true;
            this.loadingActive = false;
            this.StateHasChanged();
        }
    }

    private string BuildGeoJson(GetNetworkQueryResult data)
    {
        var stationsGeoJson = new List<string>();
        var segmentsGeoJson = new List<string>();

        foreach (var station in data.Stations)
        {
            stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": [{station.Longitude.ToString(CultureInfo.InvariantCulture)},{station.Latitude.ToString(CultureInfo.InvariantCulture)}] }}");
        }

        foreach (var segment in data.Segments)
        {
            var points = segment.Vertices.Prepend(segment.ALocation).Append(segment.BLocation);

            var line = string.Join(',', points.Select(x => $"[{x.Longitude.ToString(CultureInfo.InvariantCulture)},{x.Latitude.ToString(CultureInfo.InvariantCulture)}]"));

            segmentsGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");
        }

        return $"[{string.Join(',', segmentsGeoJson.Concat(stationsGeoJson))}]";
    }
}