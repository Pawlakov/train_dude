// <copyright file="EntityLookupPageBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Shared;

using System;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Web.Client.Components.Forms;
using TrainDude.Web.Client.Services;

public abstract class EntityLookupPageBase<TQuery, TQueryResult>
    : ComponentBase
    where TQuery : class, ILookupQuery<TQueryResult>
    where TQueryResult : ILookupQueryResult
{
    protected EntityLookupFormModel formModel;
    protected EditContext formContext;
    protected FluentValidationValidator<EntityLookupFormModel> validator;
    protected TQueryResult? queryResult = default;

    private bool loadingActive;

    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public ApiClient? Api { get; set; }

    protected override void OnInitialized()
    {
        this.formModel = new EntityLookupFormModel();
        this.formContext = new EditContext(this.formModel);
    }

    protected override async Task OnParametersSetAsync()
    {
        this.formModel.Id = this.Id;
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
                var query = this.BuildQuery(this.formModel);
                this.queryResult = await this.Api.GetAsync<TQuery, TQueryResult>(this.formModel.Id, query);

                await this.OnSubmitAsync();
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

    protected virtual Task OnSubmitAsync()
    {
        return Task.CompletedTask;
    }

    protected abstract TQuery BuildQuery(EntityLookupFormModel formModel);
}