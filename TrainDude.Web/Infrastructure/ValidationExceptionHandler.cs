// <copyright file="ValidationExceptionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Infrastructure;

using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ValidationExceptionHandler
    : IExceptionHandler
{
    private readonly ProblemDetailsFactory factory;

    public ValidationExceptionHandler(ProblemDetailsFactory factory)
    {
        this.factory = factory;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var modelState = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        var problemDetails = this.factory.CreateValidationProblemDetails(httpContext, modelState, StatusCodes.Status400BadRequest);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}