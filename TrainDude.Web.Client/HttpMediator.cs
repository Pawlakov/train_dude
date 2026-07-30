// <copyright file="HttpMediator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Newtonsoft.Json;

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
        var typeString = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.GetType().AssemblyQualifiedName));
        var requestString = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request, typeof(IRequest<TResponse>), null)));

        var response = await this.http.GetAsync($"api/mediator/requestresponse?type={typeString}&request={requestString}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errors = JsonConvert.DeserializeObject<ValidationFailure[]>(await response.Content.ReadAsStringAsync());
            throw new ValidationException(errors);
        }

        var result = JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        return result;
    }

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new CancellationToken())
        where TRequest : IRequest
    {
        var typeString = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.GetType().AssemblyQualifiedName));
        var requestString = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request, typeof(TRequest), null)));

        var response = await this.http.GetAsync($"api/mediator/request?type={typeString}&request={requestString}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errors = JsonConvert.DeserializeObject<ValidationFailure[]>(await response.Content.ReadAsStringAsync());
            throw new ValidationException(errors);
        }
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