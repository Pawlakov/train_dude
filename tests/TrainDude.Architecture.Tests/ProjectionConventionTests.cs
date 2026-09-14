// <copyright file="ProjectionConventionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;

using Marten.Events.Aggregation;
using Marten.Events.Projections;

using NetArchTest.Rules;

public class ProjectionConventionTests
{
    [Test]
    public async Task Groupers_Should_BeSealed()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("Grouper")
            .Should().BeSealed()
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    [Test]
    public async Task Groupers_Should_NotBePublic()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("Grouper")
            .Should().NotBePublic()
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    [Test]
    public async Task Groupers_Should_ImplementIAggregateGrouperOfGuid()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("Grouper")
            .Should().ImplementInterface(typeof(IAggregateGrouper<Guid>))
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    [Test]
    public async Task Projections_Should_BeSealed()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("Projection")
            .Should().BeSealed()
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    [Test]
    public async Task Projections_Should_LiveUnderAProjectionsNamespace()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("Projection")
            .Should().ResideInNamespaceContaining(".Projections")
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    [Test]
    public async Task Projections_Should_InheritFromAMartenAggregationBaseClass()
    {
        // NetArchTest can't express "inherits from one of these open-generic base classes" directly, so this one is a small hand-rolled check.
        var offenders = Assemblies.Infrastructure.GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith("Projection", StringComparison.Ordinal))
            .Where(t => !InheritsOpenGeneric(t, typeof(SingleStreamProjection<,>))
                     && !InheritsOpenGeneric(t, typeof(MultiStreamProjection<,>)))
            .Select(t => t.FullName)
            .ToList();

        await Assert.That(offenders).IsEmpty();
    }

    [Test]
    public async Task WithReferencesEvents_Should_BeSealed()
    {
        var result = Types.InAssembly(Assemblies.Infrastructure)
            .That().HaveNameEndingWith("WithReferences")
            .Should().BeSealed()
            .GetResult();

        await Assert.That(result.FailingTypeNames()).IsEmpty();
    }

    private static bool InheritsOpenGeneric(Type type, Type openGeneric)
    {
        for (var current = type.BaseType; current is not null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == openGeneric)
            {
                return true;
            }
        }

        return false;
    }
}