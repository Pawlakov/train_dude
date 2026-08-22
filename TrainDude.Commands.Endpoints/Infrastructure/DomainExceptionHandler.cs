// <copyright file="DomainExceptionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Infrastructure;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TrainDude.Domain;
using TrainDude.Domain.Base;

public class DomainExceptionHandler
    : IExceptionHandler
{
    private readonly IProblemDetailsService service;
    private readonly ILogger<DomainExceptionHandler> logger;

    public DomainExceptionHandler(IProblemDetailsService service, ILogger<DomainExceptionHandler> logger)
    {
        this.service = service;
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DomainException domainException)
        {
            return false;
        }

        this.logger.LogWarning(domainException, "Domain rule violated: {Name}", domainException.GetType().Name);

        var statusCode = domainException.StatusCode switch
        {
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        httpContext.Response.StatusCode = statusCode;
        return await this.service.TryWriteAsync(
        new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = domainException,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = "Domain rule violated",
                Detail = domainException.Message,
            },
        });
    }
}