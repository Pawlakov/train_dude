// <copyright file="NavMenu.razor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Shared;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

public partial class NavMenu
    : ComponentBase
{
    private const string DefaultBurgerClass = "navbar-burger";
    private const string DefaultMenuClass = "navbar-menu";

    private bool isActive;
    private string? burgerClass;
    private string? menuClass;

    protected override async Task OnParametersSetAsync()
    {
        this.isActive = false;
        this.burgerClass = DefaultBurgerClass;
        this.menuClass = DefaultMenuClass;

        await base.OnParametersSetAsync();
    }

    private void ToggleNavMenu()
    {
        if (this.isActive)
        {
            this.isActive = false;
            this.burgerClass = DefaultBurgerClass;
            this.menuClass = DefaultMenuClass;
        }
        else
        {
            this.isActive = true;
            this.burgerClass = $"{DefaultBurgerClass} is-active";
            this.menuClass = $"{DefaultMenuClass} is-active";
        }
    }
}