// <copyright file="EntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Components.Forms;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

using TrainDude.Queries.Requests.Base;

public abstract class EntityLookupPageBase<TQuery, TQueryResult>
    : ComponentBase
    where TQuery : BaseEntityLookupQuery<TQueryResult>, new()
    where TQueryResult : BaseEntityLookupQueryResult
{
    protected TQuery query;
    protected EditContext formContext;
    protected FluentValidationValidator<TQuery> validator;
    protected TQueryResult? queryResult = null;

    private IJSObjectReference scriptModule;
    private string? geoJson;
    private bool dataPresent;
    private bool loadingActive;

    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public ISender Mediator { get; set; }

    protected override Task OnInitializedAsync()
    {
        this.query = new TQuery();
        this.formContext = new EditContext(this.query);
        return Task.CompletedTask;
    }

    protected override async Task OnParametersSetAsync()
    {
        this.query.Id = this.Id;
        await this.Submit();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this.scriptModule = await this.JS.InvokeAsync<IJSObjectReference>("import", "./Layout/BaseMapLayout.razor.js");
        }

        if (!dataPresent && this.geoJson != null)
        {
            await this.scriptModule.InvokeVoidAsync("clearGeoJson");
            await this.scriptModule.InvokeVoidAsync("addGeoJson", this.geoJson);

            this.dataPresent = true;
            this.loadingActive = false;
            this.StateHasChanged();
        }
    }

    protected async Task Submit()
    {
        var valid = this.formContext.Validate();
        if (valid)
        {
            this.loadingActive = true;
            this.StateHasChanged();

            try
            {
                this.queryResult = await this.Mediator.Send(this.query);
            }
            catch (ValidationException exception)
            {
                this.validator.PopulateErrors(exception.Errors);
            }

            this.loadingActive = false;
            this.StateHasChanged();
        }
    }

    protected string BuildGeoJson()
    {
        var stationsGeoJson = new List<string>();
        var segmentsGeoJson = new List<string>();

        foreach (var station in this.queryResult.StationPoints)
        {
            stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": [{station.Longitude.ToString(CultureInfo.InvariantCulture)},{station.Latitude.ToString(CultureInfo.InvariantCulture)}] }}");
        }

        foreach (var segment in this.queryResult.SegmentLineStrings)
        {
            var line = string.Join(',', segment.Select(x => $"[{x.Longitude.ToString(CultureInfo.InvariantCulture)},{x.Latitude.ToString(CultureInfo.InvariantCulture)}]"));

            segmentsGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");
        }

        return $"[{string.Join(',', segmentsGeoJson.Concat(stationsGeoJson))}]";
    }
}