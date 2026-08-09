// <copyright file="FluentValidationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Validation;

using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using TrainDude.Shared.Validation;

public class FluentValidationValidator<TModel>
    : ComponentBase, IDisposable
    where TModel : class
{
    private ValidationMessageStore messages;

    [Inject]
    private IEnumerable<IInputValidator<TModel>> Validators { get; set; }

    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; }

    public void PopulateErrors(IEnumerable<ValidationFailure> errors)
    {
        this.messages.Clear();
        foreach (var error in errors)
        {
            this.messages.Add(this.CurrentEditContext.Field(error.PropertyName), error.ErrorMessage);
        }

        this.CurrentEditContext.NotifyValidationStateChanged();
    }

    protected override void OnInitialized()
    {
        if (this.CurrentEditContext == null)
        {
            throw new InvalidOperationException($"A cascading parameter of type EditContext is required.");
        }

        this.messages = new ValidationMessageStore(this.CurrentEditContext);
        this.CurrentEditContext.OnValidationRequested += this.ValidateModel;
        this.CurrentEditContext.OnFieldChanged += this.ValidateField;
    }

    private void ValidateModel(object sender, ValidationRequestedEventArgs eventArgs)
    {
        if (this.Validators.Any())
        {
            var context = new ValidationContext<TModel>(this.CurrentEditContext.Model as TModel);
            var errors = this.Validators.SelectMany(validator => validator.Validate(context).Errors).ToList();

            this.messages.Clear();
            foreach (var error in errors)
            {
                this.messages.Add(this.CurrentEditContext.Field(error.PropertyName), error.ErrorMessage);
            }

            this.CurrentEditContext.NotifyValidationStateChanged();
        }
    }

    private void ValidateField(object sender, FieldChangedEventArgs eventArgs)
    {
        if (this.Validators.Any())
        {
            var fieldIdentifier = eventArgs.FieldIdentifier;
            var properties = new[] { fieldIdentifier.FieldName };

            var context = new ValidationContext<TModel>(this.CurrentEditContext.Model as TModel, new PropertyChain(), new MemberNameValidatorSelector(properties));
            var errors = this.Validators.SelectMany(validator => validator.Validate(context).Errors);

            this.messages.Clear(fieldIdentifier);
            foreach (var error in errors)
            {
                this.messages.Add(fieldIdentifier, error.ErrorMessage);
            }

            this.CurrentEditContext.NotifyValidationStateChanged();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    void IDisposable.Dispose()
    {
        this.messages.Clear();
        this.CurrentEditContext.OnFieldChanged -= this.ValidateField;
        this.CurrentEditContext.OnValidationRequested -= this.ValidateModel;
        this.CurrentEditContext.NotifyValidationStateChanged();

        this.Dispose(true);
    }
}