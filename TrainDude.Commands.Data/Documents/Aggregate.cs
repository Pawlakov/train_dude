// <copyright file="Aggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Collections.Generic;

using Mediator;

public abstract class Aggregate
{
    private readonly List<INotification> uncommittedEvents;

    public Guid Id { get; }

    protected Aggregate(Guid id)
    {
        this.uncommittedEvents = new List<INotification>();

        this.Id = id;
    }

    public IReadOnlyCollection<INotification> UncommittedEvents => this.uncommittedEvents.AsReadOnly();

    public void ClearUncommittedEvents()
    {
        this.uncommittedEvents.Clear();
    }

    protected void AddEvent(INotification notification)
    {
        this.uncommittedEvents.Add(notification);
        this.Apply(notification);
    }

    protected abstract void Apply(INotification notification);
}