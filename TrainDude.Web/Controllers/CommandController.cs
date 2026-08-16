// <copyright file="CommandController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Base;

using Wolverine;

[Route("api/mediator/[controller]")]
[ApiController]
public class CommandController
    : ControllerBase
{
    private readonly IMessageBus bus;

    public CommandController(IMessageBus bus)
    {
        this.bus = bus;
    }

    [HttpPost]
    public async Task<ActionResult> Handle([FromBody] BasePolymorphicCommand request)
    {
        await this.bus.InvokeForTenantAsync(request);
        return this.Ok();
    }
}