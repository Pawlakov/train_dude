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

using TrainDude.Queries.Contracts.Base;
using TrainDude.Web.Client.Services;

public abstract class EntityLookupPageBase<TQuery, TQueryResult>
    : ComponentBase
    where TQuery : BaseEntityLookupQuery<TQueryResult>, new()
    where TQueryResult : BaseEntityLookupQueryResult
{
    protected TQuery query;
    protected EditContext formContext;
    protected FluentValidationValidator<TQuery> validator;
    protected TQueryResult? queryResult = null;

    private bool loadingActive;

    [Parameter]
    public Guid Id { get; set; }

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
            finally
            {
                this.loadingActive = false;
                this.StateHasChanged();
            }
        }
    }
}