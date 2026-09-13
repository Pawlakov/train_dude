// <copyright file="MapEntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Shared;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Web.Client.Services;

public abstract class MapEntityLookupPageBase<TQuery, TQueryResult, TMapQuery>
    : EntityLookupPageBase<TQuery, TQueryResult>
    where TQuery : class, ILookupQuery<TQueryResult>
    where TQueryResult : ILookupQueryResponse
    where TMapQuery : IMapQuery, ISpecificQuery<MapQueryResponse>
{
    private MapQueryResponse mapQueryResponse = default;

    [Inject]
    public MapService? MapService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != this.mapQueryResponse)
        {
            await this.MapService.ShowAsync(this.mapQueryResponse);
        }
    }

    protected override async Task OnSubmitAsync()
    {
        var query = this.BuildMapQuery(this.formModel);
        this.mapQueryResponse = await this.Api.SendAsync<TMapQuery, MapQueryResponse>(query);
    }

    protected abstract TMapQuery BuildMapQuery(EntityLookupFormModel formModel);
}