// <copyright file="BaseMapLayout.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Layout;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class BaseMapLayout
    : LayoutComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private IJSObjectReference? scriptModule;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this.scriptModule = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Layout/BaseMapLayout.razor.js");

            await this.scriptModule.InvokeVoidAsync("initMap", "map", 54.218000, 21.725389, 12);
        }
    }
}