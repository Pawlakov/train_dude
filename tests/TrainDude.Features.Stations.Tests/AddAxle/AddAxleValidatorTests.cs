// <copyright file="AddAxleValidatorTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.AddAxle;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Stations.AddAxle;
using TrainDude.Features.Stations.Contracts.AddAxle;

public class AddAxleValidatorTests
{
    [Test]
    public async Task Validate_DefaultId_Fails()
    {
        var validator = new AddAxleValidator();
        var command = new AddAxleCommand(Guid.Empty, 0);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsFalse();
        await Assert.That(result.Errors).All(x => x.PropertyName == nameof(AddAxleCommand.Id));
    }

    [Test]
    public async Task Validate_NonDefaultId_Passes()
    {
        var validator = new AddAxleValidator();
        var command = new AddAxleCommand(Guid.NewGuid(), 0);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsTrue();
        await Assert.That(result.Errors).IsEmpty();
    }
}