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
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using TrainDude.Commands.Contracts.Base;

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
        var response = await this.http.PostAsJsonAsync(command.Route, command, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var problem = await response.Content.ReadFromJsonAsync<HttpCommandSender.ValidationProblemDetailsDto>(cancellationToken: cancellationToken);

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