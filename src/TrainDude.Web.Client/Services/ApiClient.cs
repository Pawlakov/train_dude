// <copyright file="ApiClient.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using RestSharp;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Web.Client.Exceptions;

public class ApiClient
{
    private readonly IRestClient client;

    public ApiClient(IRestClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task PostAsync<TRequest>(Guid id, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ISpecificCommand
    {
        ArgumentNullException.ThrowIfNull(request);
        var route = BuildRoute(TRequest.Route, id);
        return this.SendAsync(Method.Post, route, request, null, cancellationToken);
    }

    public Task PostAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IGeneralCommand
    {
        ArgumentNullException.ThrowIfNull(request);
        return this.SendAsync(Method.Post, TRequest.Route, request, null, cancellationToken);
    }

    public Task<TResponse> GetAsync<TRequest, TResponse>(Guid id, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ISpecificQuery<TResponse>
        where TResponse : IQueryResult
    {
        ArgumentNullException.ThrowIfNull(request);
        var route = BuildRoute(TRequest.Route, id);
        return this.SendAsync<TResponse>(Method.Get, route, null, request, cancellationToken);
    }

    public Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IGeneralQuery<TResponse>
        where TResponse : IQueryResult
    {
        ArgumentNullException.ThrowIfNull(request);
        return this.SendAsync<TResponse>(Method.Get, TRequest.Route, null, request, cancellationToken);
    }

    private async Task SendAsync(Method method, string route, object? body = null, object? query = null, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(method, route, body, query);
        var response = await this.client.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            HandleErrorResponse(response);
        }
    }

    private async Task<TResponse> SendAsync<TResponse>(Method method, string route, object? body = null, object? query = null, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(method, route, body, query);
        var response = await this.client.ExecuteAsync<TResponse>(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            HandleErrorResponse(response);
        }

        return response.Data ?? throw new ApiException(response.StatusCode, "Empty Response", "The server returned an empty payload.");
    }

    private static RestRequest CreateRequest(Method method, string route, object? body, object? query)
    {
        var request = new RestRequest(route, method);
        if (body is not null)
        {
            request.AddJsonBody(body);
        }

        if (query is not null)
        {
            request.AddObject(query);
        }

        return request;
    }

    private static void HandleErrorResponse(RestResponse response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest && !string.IsNullOrWhiteSpace(response.Content))
        {
            var failures = TryParseValidationFailures(response.Content);
            if (failures is { Count: > 0 })
            {
                throw new ValidationException(failures);
            }
        }

        var problem = TryParseProblemDetails(response.Content);
        throw new ApiException(response.StatusCode, problem?.Title, problem?.Detail);
    }

    private static List<ValidationFailure>? TryParseValidationFailures(string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        try
        {
            var problem = JsonSerializer.Deserialize<ValidationProblemDetailsDto>(body);
            return problem?.Errors?.SelectMany(e => e.Value.Select(m => new ValidationFailure(e.Key, m))).ToList();
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static ProblemDetailsDto? TryParseProblemDetails(string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<ProblemDetailsDto>(body);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string BuildRoute(string template, Guid? id = null)
    {
        return id.HasValue ? template.Replace("{id}", id.Value.ToString()) : template;
    }

    private sealed record ValidationProblemDetailsDto([property: JsonPropertyName("errors")] Dictionary<string, string[]>? Errors);

    private sealed record ProblemDetailsDto([property: JsonPropertyName("title")] string? Title, [property: JsonPropertyName("detail")] string? Detail);
}