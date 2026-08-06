// <copyright file="CommandController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Base;

[Route("api/mediator/[controller]")]
[ApiController]
public class CommandController
    : ControllerBase
{
    private readonly IMediator mediator;

    public CommandController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BasePolymorphicCommandResponse>> Handle([FromBody] BasePolymorphicCommand request)
    {
        var response = await this.mediator.Send(request);
        if (response is Unit)
        {
            return this.Ok(response);
        }

        if (response is not BasePolymorphicCommandResponse polymorphicResponse)
        {
            if (response == null)
            {
                return this.NotFound();
            }

            throw new NotSupportedException($"{response.GetType()} is not a supported response type.");
        }

        return this.Ok(polymorphicResponse);
    }
}