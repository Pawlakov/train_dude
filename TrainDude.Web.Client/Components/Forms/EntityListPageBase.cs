// <copyright file="EntityListPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mediator;

using Microsoft.AspNetCore.Components;

using TrainDude.Queries.Requests.Base;

public abstract class EntityListPageBase<TQuery, TQueryResult, TQueryResultItem>
    : ComponentBase
    where TQuery : BaseEntityListQuery<TQueryResult>, new()
    where TQueryResult : BaseEntityListQueryResult<TQueryResultItem>
{
    protected IEnumerable<TQueryResultItem>? items;

    [Inject]
    public ISender Mediator { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var result = await this.Mediator.Send(new TQuery());
        this.items = result.Items.ToList();
    }
}