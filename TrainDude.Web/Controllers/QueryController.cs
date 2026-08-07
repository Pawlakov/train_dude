// <copyright file="QueryController.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Controllers;

using System;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Base;
using TrainDude.Queries.Requests.Base;

[Route("api/mediator/[controller]")]
[ApiController]
public class QueryController
    : ControllerBase
{
    private readonly IMediator mediator;

    public QueryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BasePolymorphicQueryResponse>> Handle([FromBody] BasePolymorphicQuery request)
    {
        var response = await this.mediator.Send(request);
        if (response is Unit)
        {
            return this.Ok(response);
        }

        if (response is not BasePolymorphicQueryResponse polymorphicResponse)
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