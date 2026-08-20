// <copyright file="DropCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Integration.Events.Admin;

using Wolverine;

public static class DropCommandHandler
{
    public static async Task<OutgoingMessages> HandleAsync(DropCommand command, IDocumentStore store, CancellationToken cancellationToken = default)
    {
        await store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var integrationEvent = new DroppedIntegrationEvent();

        return new OutgoingMessages { integrationEvent };
    }
}