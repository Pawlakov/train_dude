// <copyright file="BreadcrumbItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Shared;

public class BreadcrumbItem
{
    public BreadcrumbItem(string href, string iconClass, string label)
    {
        this.Href = href;
        this.IconClass = iconClass;
        this.Label = label;
    }

    public string Href { get; }

    public string IconClass { get; }

    public string Label { get; }
}