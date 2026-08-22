// <copyright file="DropEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Admin;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Integration.Events.Admin;

using Wolverine;
using Wolverine.Http;

public static class DropEndpoint
{
    [WolverinePost(DropCommand.Route)]
    public static async Task<(EmptyResponse, OutgoingMessages)> Post(DropCommand command, IDocumentStore store, CancellationToken cancellationToken = default)
    {
        await store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var response = new EmptyResponse();
        var integrationEvent = new DroppedIntegrationEvent();

        return (response, new OutgoingMessages { integrationEvent });
    }
}