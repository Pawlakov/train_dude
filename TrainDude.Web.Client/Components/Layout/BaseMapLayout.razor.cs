// <copyright file="BaseMapLayout.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Layout;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using TrainDude.Web.Client.Services;

public partial class BaseMapLayout
    : LayoutComponentBase
{
    [Inject]
    public MapService MapService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await this.MapService.InitializeAsync();
        }
    }
}