// <copyright file="ApiClient.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Web.Client.Exceptions;

public class ApiClient
{
    private readonly HttpClient http;
    private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public ApiClient(HttpClient http)
    {
        this.http = http;
    }

    public Task<TResponse> SendAsync<TRequest, TResponse>(Guid id, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IDomainRequest<TResponse>
        where TResponse : IRequestResult
    {
        throw new NotImplementedException();
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IDomainRequest<TResponse>
        where TResponse : IRequestResult
    {
        using var httpRequest = BuildRequestMessage<TRequest, TResponse>(request);
        using var httpResponse = await this.http.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        if (httpResponse.IsSuccessStatusCode)
        {
            var result = await httpResponse.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
            return result;
        }

        var body = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
        {
            var failures = TryParseValidationFailures(body);
            if (failures is { Count: > 0 })
            {
                throw new ValidationException(failures);
            }
        }

        var problem = TryParseProblemDetails(body);
        throw new ApiException(httpResponse.StatusCode, problem?.Title, problem?.Detail);
    }

    private static HttpRequestMessage BuildRequestMessage<TRequest, TResponse>(TRequest request)
        where TRequest : IDomainRequest<TResponse>
        where TResponse : IRequestResult
    {
        var route = request.Route;
        return new HttpRequestMessage(HttpMethod.Post, route)
        {
            Content = JsonContent.Create(request, options: jsonOptions),
        };
    }

    private static List<ValidationFailure>? TryParseValidationFailures(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        try
        {
            var problem = JsonSerializer.Deserialize<ValidationProblemDetailsDto>(body);
            if (problem?.Errors is not { Count: > 0 })
            {
                return null;
            }

            return problem.Errors
                .SelectMany(entry => entry.Value.Select(message => new ValidationFailure(entry.Key, message)))
                .ToList();
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static ProblemDetailsDto? TryParseProblemDetails(string body)
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

    private sealed class ValidationProblemDetailsDto
    {
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]>? Errors { get; init; }
    }

    private sealed class ProblemDetailsDto
    {
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        [JsonPropertyName("detail")]
        public string? Detail { get; init; }
    }
}