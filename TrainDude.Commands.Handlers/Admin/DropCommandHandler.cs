// <copyright file="DropCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Handlers.Admin;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using Mediator;

using TrainDude.Commands.Requests.Admin;

public sealed class DropCommandHandler
    : ICommandHandler<DropCommand>
{
    private readonly IDocumentStore store;

    public DropCommandHandler(IDocumentStore store)
    {
        this.store = store;
    }

    public async ValueTask<Unit> Handle(DropCommand command, CancellationToken cancellationToken)
    {
        await this.store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        return Unit.Value;
    }
}