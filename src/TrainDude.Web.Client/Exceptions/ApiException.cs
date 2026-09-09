// <copyright file="ApiException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Exceptions;

using System;
using System.Net;

using TrainDude.Web.Client.Services;

public sealed class ApiException
    : Exception
{
    public ApiException(HttpStatusCode statusCode, string? title, string? detail)
        : base(BuildMessage(statusCode, title, detail))
    {
        this.StatusCode = statusCode;
        this.Title = title;
        this.Detail = detail;
    }

    /// <summary>
    /// Gets the HTTP status code returned by the command endpoint.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the problem-details title, if the server sent one.
    /// </summary>
    public string? Title { get; }

    /// <summary>
    /// Gets the problem-details detail message, if the server sent one.
    /// </summary>
    public string? Detail { get; }

    private static string BuildMessage(HttpStatusCode statusCode, string? title, string? detail)
    {
        return detail ?? title ?? $"The command failed with status code {(int)statusCode} ({statusCode}).";
    }
}