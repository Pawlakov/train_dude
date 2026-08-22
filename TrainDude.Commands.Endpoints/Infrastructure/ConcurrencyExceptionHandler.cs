// <copyright file="ConcurrencyExceptionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Infrastructure;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class ConcurrencyExceptionHandler
    : IExceptionHandler
{
    private readonly IProblemDetailsService service;
    /*private readonly ILogger logger;*/

    public ConcurrencyExceptionHandler(IProblemDetailsService service /*, ILogger logger*/)
    {
        this.service = service;
        /*this.logger = logger;*/
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ConcurrencyException concurrencyException)
        {
            return false;
        }

        // TODO once some kind of logging is added
        /*this.logger.LogWarning(concurrencyException, "Domain rule violated: {Code}", concurrencyException.Code);*/

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        return await this.service.TryWriteAsync(
        new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = concurrencyException,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Concurrent modification",
                Detail = "This resource was modified by someone else since it was last fetched. Refresh and try again.",
            },
        });
    }
}