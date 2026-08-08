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
        messages.Clear();
        foreach (var error in errors)
        {
            messages.Add(CurrentEditContext.Field(error.PropertyName), error.ErrorMessage);
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"A cascading parameter of type EditContext is required.");
        }

        messages = new ValidationMessageStore(CurrentEditContext);
        CurrentEditContext.OnValidationRequested += ValidateModel;
        CurrentEditContext.OnFieldChanged += ValidateField;
    }

    private void ValidateModel(object sender, ValidationRequestedEventArgs eventArgs)
    {
        if (Validators.Any())
        {
            var context = new ValidationContext<TModel>(CurrentEditContext.Model as TModel);
            var errors = Validators.SelectMany(validator => validator.Validate(context).Errors).ToList();

            messages.Clear();
            foreach (var error in errors)
            {
                messages.Add(CurrentEditContext.Field(error.PropertyName), error.ErrorMessage);
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }

    private void ValidateField(object sender, FieldChangedEventArgs eventArgs)
    {
        if (Validators.Any())
        {
            var fieldIdentifier = eventArgs.FieldIdentifier;
            var properties = new[] { fieldIdentifier.FieldName };

            var context = new ValidationContext<TModel>(CurrentEditContext.Model as TModel, new PropertyChain(), new MemberNameValidatorSelector(properties));
            var errors = Validators.SelectMany(validator => validator.Validate(context).Errors);

            messages.Clear(fieldIdentifier);
            foreach (var error in errors)
            {
                messages.Add(fieldIdentifier, error.ErrorMessage);
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    void IDisposable.Dispose()
    {
        messages.Clear();
        CurrentEditContext.OnFieldChanged -= ValidateField;
        CurrentEditContext.OnValidationRequested -= ValidateModel;
        CurrentEditContext.NotifyValidationStateChanged();

        Dispose(true);
    }
}