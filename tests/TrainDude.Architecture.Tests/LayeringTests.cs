// <copyright file="LayeringTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;

using NetArchTest.Rules;

public class LayeringTests
{
    [Test]
    public async Task Infrastructure_Should_NotReferenceHostOrApiAssemblies()
    {
        var forbidden = new[] { "TrainDude.Web.Client", "TrainDude.Web" };

        var offenders = Assemblies.Infrastructure.GetReferencedAssemblies()
            .Select(a => a.Name ?? string.Empty)
            .Where(name => forbidden.Any(prefix => name.StartsWith(prefix, StringComparison.Ordinal)))
            .ToList();

        await Assert.That(offenders).IsEmpty();
    }
}