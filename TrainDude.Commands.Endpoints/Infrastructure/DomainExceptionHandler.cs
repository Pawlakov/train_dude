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

using TrainDude.Domain.Exceptions;

public class DomainExceptionHandler
    : IExceptionHandler
{
    private readonly IProblemDetailsService service;
    /*private readonly ILogger logger;*/

    public DomainExceptionHandler(IProblemDetailsService service /*, ILogger logger*/)
    {
        this.service = service;
        /*this.logger = logger;*/
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DomainException domainException)
        {
            return false;
        }

        // TODO once some kind of logging is added
        /*this.logger.LogWarning(domainException, "Domain rule violated: {Code}", domainException.Code);*/

        httpContext.Response.StatusCode = domainException.StatusCode;
        return await this.service.TryWriteAsync(
        new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = domainException,
            ProblemDetails = new ProblemDetails
            {
                Status = domainException.StatusCode,
                Title = "Domain rule violated",
                Detail = domainException.Message,
            },
        });
    }
}