// <copyright file="UnprocessableEntityProblemDetailSource.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Wolverine.Http.FluentValidation;

public sealed class UnprocessableEntityProblemDetailSource<TMessage>
    : IProblemDetailSource<TMessage>
{
    public ProblemDetails Create(TMessage message, IReadOnlyList<ValidationFailure> failures)
    {
        var errors = failures
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray());

        return new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Validation failed",
        };
    }
}