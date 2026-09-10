// <copyright file="NetworkMap.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Network;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Features.Segments.Contracts.GetSegmentsMap;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStationsMap;
using TrainDude.Web.Client.Services;

public partial class NetworkMap
    : ComponentBase
{
    private MapQueryResult? queryResult;

    [Inject]
    public ApiClient? Api { get; set; }

    [Inject]
    public MapService MapService { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var stationsResult = await this.Api.GetAsync<GetStationsMapQuery, MapQueryResult>(new GetStationsMapQuery());
        var segmentsResult = await this.Api.GetAsync<GetSegmentsMapQuery, MapQueryResult>(new GetSegmentsMapQuery());
        this.queryResult = new(stationsResult.StationPoints, segmentsResult.SegmentLineStrings);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != this.queryResult)
        {
            await this.MapService.ShowAsync(this.queryResult);
        }
    }
}