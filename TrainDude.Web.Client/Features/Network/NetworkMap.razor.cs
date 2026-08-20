// <copyright file="NetworkMap.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Web.Client.Features.Network;

using System;
using System.Threading.Tasks;

using Mediator;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using TrainDude.Queries.Requests.Network;
using TrainDude.Web.Client.Services;

public partial class NetworkMap
    : ComponentBase
{
    private bool loadingActive;
    private GetNetworkQueryResult? queryResult;

    [Inject]
    public ISender Mediator { get; set; }

    [Inject]
    public MapService MapService { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        this.loadingActive = true;
        this.StateHasChanged();

        this.queryResult = await this.Mediator.Send(new GetNetworkQuery());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != this.queryResult)
        {
            await this.MapService.ShowAsync(this.queryResult);

            this.loadingActive = false;
            this.StateHasChanged();
        }
    }
}