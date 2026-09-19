// <copyright file="SetLocationValidatorTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.SetLocation;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.SetLocation;

public class SetLocationValidatorTests
{
    [Test]
    public async Task Validate_DefaultLocation_Fails()
    {
        var validator = new SetLocationValidator();
        var command = new SetLocationCommand(Guid.NewGuid(), default);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsFalse();
        await Assert.That(result.Errors).All(x => x.PropertyName == nameof(SetLocationCommand.Location));
    }

    [Test]
    public async Task Validate_DefaultId_Fails()
    {
        var validator = new SetLocationValidator();
        var command = new SetLocationCommand(Guid.Empty, new Location(13, 37));

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsFalse();
        await Assert.That(result.Errors).All(x => x.PropertyName == nameof(SetLocationCommand.Id));
    }

    [Test]
    public async Task Validate_NonDefaultLocationAndId_Passes()
    {
        var validator = new SetLocationValidator();
        var command = new SetLocationCommand(Guid.NewGuid(), new Location(20, 50));

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsTrue();
        await Assert.That(result.Errors).IsEmpty();
    }
}