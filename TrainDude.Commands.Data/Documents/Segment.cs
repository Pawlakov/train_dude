// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Segments;

public class Segment
    : Aggregate
{
    [JsonConstructor]
    private Segment(Guid id)
        : base(id)
    {
    }

    public static Segment Create(Guid segmentId)
    {
        var segment = new Segment(segmentId);
        segment.AddEvent(new SegmentCreatedNotification(segmentId));
        return segment;
    }

    protected override void Apply(INotification notification)
    {
        switch (notification)
        {
            default:
                throw new NotSupportedException("This event type is not meant for this aggregate.");
        }
    }
}