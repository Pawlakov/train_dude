// <copyright file="CreateTripCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Trips;

using System;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Mediator;

using TrainDude.Commands.Requests.Trips;
using TrainDude.Domain.Documents;

public sealed class CreateTripCommandHandler
    : ICommandHandler<CreateTripCommand, CreateTripCommandResult>
{
    private readonly IDocumentSession session;

    public CreateTripCommandHandler(IDocumentSession session)
    {
        this.session = session;
    }

    public async ValueTask<CreateTripCommandResult> Handle(CreateTripCommand command, CancellationToken cancellationToken)
    {
        var tripId = Guid.NewGuid();

        var created = Trip.Make(tripId, command.Number);

        this.session.Events.StartStream<Trip>(tripId, created);
        await this.session.SaveChangesAsync(cancellationToken);

        return new CreateTripCommandResult
        {
            Id = tripId,
        };
    }
}