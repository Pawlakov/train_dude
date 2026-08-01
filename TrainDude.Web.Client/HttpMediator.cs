// <copyright file="HttpMediator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using Mediator;

using TrainDude.Application.Requests.Base;

public class HttpMediator
    : IMediator
{
    private readonly HttpClient http;

    public HttpMediator(HttpClient http)
    {
        this.http = http;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException("IRequest is not supported. Use ICommand or IQuery.");
    }

    public async ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (command is not BasePolymorphicCommand polymorphicRequest)
        {
            throw new NotSupportedException($"This request type is not supporting polymorphic JSON serialization. The type is {command.GetType()}.");
        }

        var response = await this.http.PostAsJsonAsync("api/mediator/command", polymorphicRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await response.Content.ReadFromJsonAsync<ValidationFailure[]>();
                throw new ValidationException(errors);
            }
            else
            {
                throw new ApplicationException("A request to the mediator endpoint returned a status code indicating failure.");
            }
        }

        var result = await response.Content.ReadFromJsonAsync<TResponse>();
        return result ?? throw new InvalidOperationException("Missing response.");
    }

    public async ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = new CancellationToken())
    {
        if (query is not BasePolymorphicQuery polymorphicRequest)
        {
            throw new NotSupportedException($"This request type is not supporting polymorphic JSON serialization. The type is {query.GetType()}.");
        }

        var response = await this.http.PostAsJsonAsync("api/mediator/query", polymorphicRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = await response.Content.ReadFromJsonAsync<ValidationFailure[]>();
                throw new ValidationException(errors);
            }
            else
            {
                throw new ApplicationException("A request to the mediator endpoint returned a status code indicating failure.");
            }
        }

        var result = await response.Content.ReadFromJsonAsync<TResponse>();
        return result ?? throw new InvalidOperationException("Missing response.");
    }

    /* Savage fields below. */

    public async ValueTask<object?> Send(object message, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamQuery<TResponse> query, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamCommand<TResponse> command, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

    public IAsyncEnumerable<object?> CreateStream(object message, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

    async ValueTask<TResponse> ISender.Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async ValueTask Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification => throw new NotImplementedException();

    public async ValueTask Publish(object notification, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();
}