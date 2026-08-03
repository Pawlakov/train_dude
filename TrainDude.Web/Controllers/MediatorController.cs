// <copyright file="MediatorController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Base;
using TrainDude.Queries.Requests.Base;

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
    public async Task<ActionResult<BasePolymorphicCommandResponse>> Command([FromBody] BasePolymorphicCommand request)
    {
        try
        {
            var response = await this.mediator.Send(request);
            if (response is not BasePolymorphicCommandResponse polymorphicResponse)
            {
                return this.BadRequest($"{response.GetType()} is not a supported response type.");
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
    public async Task<ActionResult<BasePolymorphicCommandResponse>> Query([FromBody] BasePolymorphicQuery request)
    {
        try
        {
            var response = await this.mediator.Send(request);
            if (response is not BasePolymorphicCommandResponse polymorphicResponse)
            {
                return this.BadRequest($"{response.GetType()} is not a supported response type.");
            }

            return this.Ok(polymorphicResponse);
        }
        catch (ValidationException exception)
        {
            return this.BadRequest(JsonSerializer.Serialize(exception.Errors));
        }
        catch // TODO Jakiś lepszy handling tego co się wywaliło.
        {
            throw;
        }
    }
}