// <copyright file="MapEntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Queries.Requests.Base;
using TrainDude.Web.Client.Services;

public abstract class MapEntityLookupPageBase<TQuery, TQueryResult>
    : EntityLookupPageBase<TQuery, TQueryResult>
    where TQuery : BaseEntityLookupQuery<TQueryResult>, new()
    where TQueryResult : BaseEntityLookupQueryResult, IMapQueryResult
{
    [Inject]
    public MapService MapService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (this.queryResult != null && this.MapService.CurrentData != this.queryResult)
        {
            await this.MapService.ShowAsync(this.queryResult);
        }
    }
}