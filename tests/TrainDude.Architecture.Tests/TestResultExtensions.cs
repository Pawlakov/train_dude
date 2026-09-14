// <copyright file="TestResultExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Tests;

using System;
using System.Linq;

using NetArchTest.Rules;

internal static class TestResultExtensions
{
    public static string[] FailingTypeNames(this TestResult result)
    {
        return (result.FailingTypes ?? Enumerable.Empty<Type>())
            .Select(t => t.FullName ?? t.Name)
            .ToArray();
    }
}