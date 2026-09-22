// <copyright file="ValidationExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Extensions;

using System.Linq;

using FluentValidation;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> MustBeValidStationName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        where T : class
    {
        return ruleBuilder
            .Length(1, 64)
            .Must(x => !x.Any(char.IsControl)).WithMessage("'{PropertyName}' cannot contain line breaks or hidden control characters.")
            .Must(x => x == x?.Trim()).WithMessage("'{PropertyName}' must not begin or end with whitespace.")
            .Must(x => !x.Contains("  ")).WithMessage("'{PropertyName}' cannot contain consecutive spaces.")
            .Matches(@"^[\p{L} \-\(\)\.]+$").WithMessage("'{PropertyName}' can only contain letters, spaces, hyphens, parentheses and points.");
    }
}