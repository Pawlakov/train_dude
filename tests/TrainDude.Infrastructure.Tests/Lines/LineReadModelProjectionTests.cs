// <copyright file="LineReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class LineReadModelProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    [Arguments(120, null, "120")]
    [Arguments(110, 'f', "110f")]
    public async Task LineCreated(int lineNumber, char? lineLetter, string lineDesignation)
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(lineId, new LineCreated(lineId, Who, lineNumber, lineLetter));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.LineNumber).IsEqualTo(lineNumber);
            await Assert.That(line.LineLetter).IsEqualTo(lineLetter);
            await Assert.That(line.LineDesignation).IsEqualTo(lineDesignation);
        }
    }
}