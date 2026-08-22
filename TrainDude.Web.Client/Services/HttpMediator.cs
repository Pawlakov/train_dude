// <copyright file="HttpMediator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using Mediator;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Queries.Contracts.Base;

public class HttpMediator
    : ISender
{
    private readonly HttpClient http;

    public HttpMediator(HttpClient http)
    {
        this.http = http;
    }

    public async ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = new CancellationToken())
    {
        if (query is not BasePolymorphicQuery polymorphicRequest)
        {
            throw new NotSupportedException($"This request type is not supporting polymorphic JSON serialization. The type is {query.GetType()}.");
        }

        var response = await this.http.PostAsJsonAsync("api/mediator/query", polymorphicRequest, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<TResponse>();
        return result ?? throw new InvalidOperationException("Missing response.");
    }

    /* Savage fields below. */

    public ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Commands using Mediator are no longer supported. Use HttpCommandSender.");
    }

    public ValueTask<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("IRequest is not supported. Use IQuery.");
    }

    public ValueTask<object?> Send(object message, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamCommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<object?> CreateStream(object message, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    /* Savage field above */

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(cancellationToken: cancellationToken);

            var failures = (problem?.Errors ?? [])
                .SelectMany(entry => entry.Value.Select(message => new ValidationFailure(entry.Key, message)))
                .ToList();

            throw new ValidationException(failures);
        }

        throw new ApplicationException("A request to the mediator endpoint returned a status code indicating failure.");
    }

    private sealed class ValidationProblemDetailsDto
    {
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]>? Errors { get; init; }
    }
}