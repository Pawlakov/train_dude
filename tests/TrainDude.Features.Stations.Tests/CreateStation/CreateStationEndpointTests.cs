// <copyright file="CreateStationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.CreateStation;

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Alba;

using Marten;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class CreateStationEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    [Arguments("German New", "Polish", null)]
    [Arguments("German New", null, null)]
    [Arguments("German New", null, "Russian")]
    [Arguments(null, null, "Russian")]
    [Arguments(null, null, null)]
    [Arguments(null, "Polish", null)]
    public async Task Post_Valid_Returns201AndAppendsEvent(string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        var result = await this.PostCreateCommandAsync(new CreateStationCommand("German", nameGermanNew, namePolish, nameRussian), 201);

        // Response
        var response = await result.ReadAsJsonAsync<CreatedResponse>();
        var stationId = response.Id;

        // Response Header
        var locationPattern = new Regex(GetStationQuery.Route.Replace("{id}", @"(?<id>[0-9a-fA-F]{8}-(?:[0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12})") + "$");
        var location = result.Context.Response.Headers.Location;
        await Assert.That(location).IsNotNull();
        var match = locationPattern.Match(location.ToString());
        await Assert.That(match.Success).IsTrue();
        await Assert.That(Guid.Parse(match.Groups["id"].Value)).IsEqualTo(stationId);

        // Even Store
        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(1);
        var stationCreated = (StationCreated?)events.Single().Data;
        await Assert.That(stationCreated).IsNotNull();

        await Assert.That(stationCreated.StationId).IsEqualTo(stationId);
        await Assert.That(stationCreated.Who).IsEqualTo(this.AuthFixture.AuthenticatedActor);
        await Assert.That(stationCreated.NameGerman).IsEqualTo("German");
        await Assert.That(stationCreated.NameGermanNew).IsEqualTo(nameGermanNew);
        await Assert.That(stationCreated.NamePolish).IsEqualTo(namePolish);
        await Assert.That(stationCreated.NameRussian).IsEqualTo(nameRussian);
    }

    [Test]
    public async Task Post_WithoutGermanName_Returns422()
    {
        var result = await this.PostCreateCommandAsync(new CreateStationCommand(null, null, null, "Russian"), 422);

        var problem = await result.ReadAsJsonAsync<ValidationProblemDetails>();
        await Assert.That(problem.Errors).ContainsKey(nameof(CreateStationCommand.NameGerman));
        await Assert.That(problem.Errors).AllKeys(x => x is nameof(CreateStationCommand.NameGerman));
        await this.AssertNothingWrittenAsync();
    }

    [Test]
    [Arguments("", null, "Polish", null, nameof(CreateStationCommand.NameGerman))]
    [Arguments("\t \t", null, "Polish", null, nameof(CreateStationCommand.NameGerman))]
    [Arguments(" German", null, "Polish", null, nameof(CreateStationCommand.NameGerman))]
    [Arguments("German ", null, "Polish", null, nameof(CreateStationCommand.NameGerman))]
    [Arguments("German", "", "Polish", null, nameof(CreateStationCommand.NameGermanNew))]
    [Arguments("German", "\t \t", "Polish", null, nameof(CreateStationCommand.NameGermanNew))]
    [Arguments("German", " German New", "Polish", null, nameof(CreateStationCommand.NameGermanNew))]
    [Arguments("German", "German New ", "Polish", null, nameof(CreateStationCommand.NameGermanNew))]
    [Arguments("German", null, "", null, nameof(CreateStationCommand.NamePolish))]
    [Arguments("German", null, "\t \t", null, nameof(CreateStationCommand.NamePolish))]
    [Arguments("German", null, " Polish", null, nameof(CreateStationCommand.NamePolish))]
    [Arguments("German", null, "Polish ", null, nameof(CreateStationCommand.NamePolish))]
    [Arguments("German", null, null, "", nameof(CreateStationCommand.NameRussian))]
    [Arguments("German", null, null, "\t \t", nameof(CreateStationCommand.NameRussian))]
    [Arguments("German", null, null, " Russian", nameof(CreateStationCommand.NameRussian))]
    [Arguments("German", null, null, "Russian ", nameof(CreateStationCommand.NameRussian))]
    public async Task Post_WithMalformedNames_Returns422(string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian, string offender)
    {
        var result = await this.PostCreateCommandAsync(new CreateStationCommand(nameGerman, nameGermanNew, namePolish, nameRussian), 422);

        var problem = await result.ReadAsJsonAsync<ValidationProblemDetails>();
        await Assert.That(problem.Errors).ContainsKey(offender);
        await Assert.That(problem.Errors).AllKeys(x => x == offender);
        await this.AssertNothingWrittenAsync();
    }

    [Test]
    [Arguments("German New")]
    [Arguments(null)]
    public async Task Post_WithBothPolishAndRussianNames_Returns422(string? nameGermanNew)
    {
        var result = await this.PostCreateCommandAsync(new CreateStationCommand("German", nameGermanNew, "Polish", "Russian"), 422);

        var problem = await result.ReadAsJsonAsync<ValidationProblemDetails>();
        await Assert.That(problem.Errors).ContainsKey(nameof(CreateStationCommand.NamePolish));
        await Assert.That(problem.Errors).ContainsKey(nameof(CreateStationCommand.NameRussian));
        await Assert.That(problem.Errors).AllKeys(x => x is nameof(CreateStationCommand.NamePolish) or nameof(CreateStationCommand.NameRussian));
        await this.AssertNothingWrittenAsync();
    }

    [Test]
    [Arguments("")]
    [Arguments("{ invalid json")]
    [Arguments("{ \"Valid\": \"syntactically but not semantically\" }")]
    [Arguments("{ \"NameGerman\": 67 }")]
    public async Task Post_WithMalformedJson_Returns400(string text)
    {
        await this.AuthFixture.Host.Scenario(x =>
        {
            x.Post.Text(text).ContentType("application/json").ToUrl(CreateStationCommand.Route);
            x.StatusCodeShouldBe(400);
        });

        await this.AssertNothingWrittenAsync();
    }

    [Test]
    public async Task Post_Unauthenticated_Returns302()
    {
        await this.PostCreateCommandAsync(new CreateStationCommand("German", "German New", "Polish", null), 302, false);

        await this.AssertNothingWrittenAsync(false);
    }
}