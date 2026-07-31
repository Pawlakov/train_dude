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

using MediatR;

using TrainDude.Application.Requests.Base;

public class HttpMediator
    : IMediator
{
    private readonly HttpClient http;

    public HttpMediator(HttpClient http)
    {
        this.http = http;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
    {
        if (request is not BaseClientRequest polymorphicRequest)
        {
            throw new NotSupportedException($"This request type is not supporting polymorphic JSON serialization. The type is {request.GetType()}.");
        }

        var response = await this.http.PostAsJsonAsync("api/mediator/with", polymorphicRequest, cancellationToken);
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

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new CancellationToken())
        where TRequest : IRequest
    {
        if (request is not BaseClientRequest polymorphicRequest)
        {
            throw new NotSupportedException($"This request type is not supporting polymorphic JSON serialization. The type is {request.GetType()}.");
        }

        var response = await this.http.PostAsJsonAsync("api/mediator/without", polymorphicRequest, cancellationToken);
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

        response.EnsureSuccessStatusCode();
    }

    public Task<object?> Send(object request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
    {
        throw new NotSupportedException();
    }
}