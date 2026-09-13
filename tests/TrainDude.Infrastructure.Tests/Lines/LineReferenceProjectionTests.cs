// <copyright file="LineReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Trips.Domain.Events;
using TrainDude.Infrastructure.Lines.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class LineReferenceProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineReferenceProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task LineTripAssigned_CreatesLineTripLink()
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(tripId, CreateTripCreated(tripId, 123));
            session.Events.Append(lineId, new LineTripAssigned(lineId, Who, tripId));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var link = await session.LoadAsync<LineTripLink>(lineId);

            await Assert.That(link).IsNotNull();
            await Assert.That(link.Id).IsEqualTo(lineId);
            await Assert.That(link.TripId).IsEqualTo(tripId);
        }
    }

    [Test]
    public async Task TripCreated_CreatesLineTripReference()
    {
        await this.fixture.ResetAsync();
        var tripId = Guid.NewGuid();

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(tripId, CreateTripCreated(tripId, 123));
        await session.SaveChangesAsync();

        var reference = await session.LoadAsync<LineTripReference>(tripId);

        await Assert.That(reference).IsNotNull();
        await Assert.That(reference.Id).IsEqualTo(tripId);
        await Assert.That(reference.Number).IsEqualTo(123);
    }

    [Test]
    public async Task SegmentCreated_CreatesLineSegmentReference()
    {
        await this.fixture.ResetAsync();
        var segmentId = Guid.NewGuid();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(
        segmentId,
        new SegmentCreated(
        segmentId,
        Who,
        22.2,
        2,
        new TrainDude.Features.Segments.Domain.Values.SegmentEnd(station1Id, 0, true),
        new TrainDude.Features.Segments.Domain.Values.SegmentEnd(station2Id, 0, true)));
        await session.SaveChangesAsync();

        var reference = await session.LoadAsync<LineSegmentReference>(segmentId);

        await Assert.That(reference).IsNotNull();
        await Assert.That(reference.Id).IsEqualTo(segmentId);
    }

    private static TripCreated CreateTripCreated(Guid tripId, int tripNumber)
    {
        return (TripCreated)CreateRecord(
        typeof(TripCreated),
        new Dictionary<string, object?>
        {
            ["id"] = tripId,
            ["tripId"] = tripId,
            ["who"] = Who,
            ["tripNumber"] = tripNumber,
        });
    }

    private static object CreateRecord(Type type, IReadOnlyDictionary<string, object?> values)
    {
        foreach (var constructor in type.GetConstructors())
        {
            var parameters = constructor.GetParameters();
            var arguments = new object?[parameters.Length];
            var supported = true;

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (TryGetValue(values, parameter.Name!, out var value))
                {
                    arguments[i] = value;
                    continue;
                }

                if (parameter.HasDefaultValue)
                {
                    arguments[i] = parameter.DefaultValue;
                    continue;
                }

                if (parameter.ParameterType == typeof(Guid))
                {
                    arguments[i] = Guid.NewGuid();
                }
                else if (parameter.ParameterType == typeof(string))
                {
                    arguments[i] = Who;
                }
                else if (parameter.ParameterType == typeof(DateTime))
                {
                    arguments[i] = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                }
                else if (parameter.ParameterType == typeof(int))
                {
                    arguments[i] = 0;
                }
                else if (parameter.ParameterType == typeof(long))
                {
                    arguments[i] = 0L;
                }
                else if (parameter.ParameterType == typeof(bool))
                {
                    arguments[i] = false;
                }
                else if (parameter.ParameterType.IsEnum)
                {
                    arguments[i] = Activator.CreateInstance(parameter.ParameterType);
                }
                else
                {
                    supported = false;
                    break;
                }
            }

            if (supported)
            {
                return constructor.Invoke(arguments);
            }
        }

        throw new InvalidOperationException($"Could not construct {type.FullName} using the supplied values.");
    }

    private static bool TryGetValue(IReadOnlyDictionary<string, object?> values, string name, out object? value)
    {
        foreach (var pair in values)
        {
            if (string.Equals(pair.Key, name, StringComparison.OrdinalIgnoreCase))
            {
                value = pair.Value;
                return true;
            }
        }

        value = null;
        return false;
    }
}