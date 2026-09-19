// <copyright file="CreateStationValidatorTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.CreateStation;

using System.Threading.Tasks;

using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.CreateStation;

public class CreateStationValidatorTests
{
    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("\t \t")]
    public async Task Validate_NameGermanNullEmptyOrWhitespace_Fails(string nameGerman)
    {
        var validator = new CreateStationValidator();
        var command = new CreateStationCommand(nameGerman, null, "Polish", null);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsFalse();
        await Assert.That(result.Errors).All(x => x.PropertyName == nameof(CreateStationCommand.NameGerman));
    }

    [Test]
    public async Task Validate_NamePolishAndNameRussianBothEmpty_Passes()
    {
        var validator = new CreateStationValidator();
        var command = new CreateStationCommand("German", null, null, null);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsTrue();
        await Assert.That(result.Errors).IsEmpty();
    }

    [Test]
    [Arguments("Polish", null)]
    [Arguments(null, "Russian")]
    public async Task Validate_OnlyOneOfNamePolishOrNameRussianSet_Passes(string? polish, string? russian)
    {
        var validator = new CreateStationValidator();
        var command = new CreateStationCommand("German", null, polish, russian);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsTrue();
        await Assert.That(result.Errors).IsEmpty();
    }

    [Test]
    public async Task Validate_BothNamePolishAndNameRussianSet_FailsOnBothProperties()
    {
        var validator = new CreateStationValidator();
        var command = new CreateStationCommand("German", null, "Polish", "Russian");

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsFalse();
        await Assert.That(result.Errors).All(x => x.PropertyName == nameof(CreateStationCommand.NamePolish) || x.PropertyName == nameof(CreateStationCommand.NameRussian));
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("\t \t")]
    [Arguments("German New")]
    public async Task Validate_NameGermanNew_IsNeverValidated(string? germanNew)
    {
        var validator = new CreateStationValidator();
        var command = new CreateStationCommand("German", germanNew, "Polish", null);

        var result = await validator.ValidateAsync(command);

        await Assert.That(result.IsValid).IsTrue();
        await Assert.That(result.Errors).IsEmpty();
    }
}