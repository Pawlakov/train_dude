// <copyright file="DropCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using Marten;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Integration.Events.Admin;

using Wolverine;

public static class DropCommandHandler
{
    public static OutgoingMessages Handle(DropCommand command, IDocumentStore store)
    {
        store.Advanced.Clean.CompletelyRemoveAllAsync().Wait();

        var integrationEvent = new DroppedIntegrationEvent();

        return new OutgoingMessages { integrationEvent };
    }
}