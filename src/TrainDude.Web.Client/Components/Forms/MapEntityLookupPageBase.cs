// <copyright file="MapEntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Web.Client.Services;

public abstract class MapEntityLookupPageBase<TQuery, TQueryResult>
    : EntityLookupPageBase<TQuery, TQueryResult>
    where TQuery : class, ILookupQuery<TQueryResult>
    where TQueryResult : ILookupQueryResult, IMapQueryResult
{
    [Inject]
    public MapService? MapService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != (IMapQueryResult)this.queryResult)
        {
            await this.MapService.ShowAsync(this.queryResult);
        }
    }
}