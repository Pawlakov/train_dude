// <copyright file="MediatorController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Application.Requests.Base;

[Route("api/[controller]")]
[ApiController]
public class MediatorController
    : ControllerBase
{
    private readonly IMediator mediator;

    public MediatorController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("command")]
    public async Task<ActionResult<BasePolymorphicResponse>> Command([FromBody] BasePolymorphicCommand request)
    {
        try
        {
            var response = await this.mediator.Send(request);
            if (response is not BasePolymorphicResponse polymorphicResponse)
            {
                throw new NotSupportedException("This response type is not supporting polymorphic JSON serialization.");
            }

            return this.Ok(polymorphicResponse);
        }
        catch (ValidationException exception)
        {
            return this.BadRequest(JsonSerializer.Serialize(exception.Errors));
        }
        catch
        {
            throw;
        }
    }

    [HttpPost("query")]
    public async Task<ActionResult<BasePolymorphicResponse>> Query([FromBody] BasePolymorphicQuery request)
    {
        try
        {
            var response = await this.mediator.Send(request);
            if (response is not BasePolymorphicResponse polymorphicResponse)
            {
                throw new NotSupportedException("This response type is not supporting polymorphic JSON serialization.");
            }

            return this.Ok(polymorphicResponse);
        }
        catch (ValidationException exception)
        {
            return this.BadRequest(JsonSerializer.Serialize(exception.Errors));
        }
        catch
        {
            throw;
        }
    }
}