// <copyright file="HttpCommandSender.cs" company="Pawlakov">
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

using TrainDude.Commands.Contracts.Base;
using TrainDude.Web.Client.Exceptions;

public class HttpCommandSender
{
    private readonly HttpClient http;

    public HttpCommandSender(HttpClient http)
    {
        this.http = http;
    }

    public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : BaseRoutedCommand
    {
        var response = await this.http.PostAsJsonAsync(command.Route, command, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var failures = TryParseValidationFailures(body);
            if (failures is { Count: > 0 })
            {
                throw new ValidationException(failures);
            }
        }

        var problem = TryParseProblemDetails(body);
        throw new CommandFailedException(response.StatusCode, problem?.Title, problem?.Detail);
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