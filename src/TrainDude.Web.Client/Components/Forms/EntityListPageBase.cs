// <copyright file="EntityListPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Web.Client.Services;

public abstract class EntityListPageBase<TQuery, TQueryResult, TQueryResultItem>
    : ComponentBase
    where TQuery : IListQuery<TQueryResult>, new()
    where TQueryResult : IListQueryResult<TQueryResultItem>
{
    protected IEnumerable<TQueryResultItem>? items;

    [Inject]
    public ApiClient? Api { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var result = await this.Api.GetAsync<TQuery, TQueryResult>(new TQuery());
        this.items = [.. result.Items];
    }
}