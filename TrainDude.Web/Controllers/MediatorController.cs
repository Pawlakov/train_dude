// <copyright file="MediatorController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

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

    [HttpPost("with")]
    public async Task<ActionResult<BaseClientResponse>> RequestWithResponse([FromBody] BaseClientRequest request)
    {
        try
        {
            var response = await this.mediator.Send(request);
            if (response is not BaseClientResponse polymorphicResponse)
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

    [HttpPost("without")]
    public async Task<ActionResult> RequestWithoutResponse([FromBody] BaseClientRequest request)
    {
        try
        {
            await this.mediator.Send(request);
            return this.Ok();
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