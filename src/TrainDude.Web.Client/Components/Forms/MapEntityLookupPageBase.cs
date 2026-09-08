// <copyright file="MapEntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Components;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Web.Client.Services;

public abstract class MapEntityLookupPageBase<TQuery, TQueryResult, TMapQuery>
    : EntityLookupPageBase<TQuery, TQueryResult>
    where TQuery : class, ILookupQuery<TQueryResult>
    where TQueryResult : ILookupQueryResult
    where TMapQuery : IMapQuery
{
    private MapQueryResult mapQueryResult = default;

    [Inject]
    public MapService? MapService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != this.mapQueryResult)
        {
            await this.MapService.ShowAsync(this.mapQueryResult);
        }
    }

    protected override async Task OnSubmitAsync()
    {
        var query = this.BuildMapQuery(this.formModel);
        this.mapQueryResult = await this.Api.SendAsync<TMapQuery, MapQueryResult>(query);
    }

    protected abstract TMapQuery BuildMapQuery(EntityLookupFormModel formModel);
}