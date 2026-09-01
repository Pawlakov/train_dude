// <copyright file="IDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System.Text.Json.Serialization;

using TrainDude.Features.Admin;
using TrainDude.Features.Lines.AppendSegment;
using TrainDude.Features.Lines.AssignTrip;
using TrainDude.Features.Lines.CreateLine;
using TrainDude.Features.Segments.CreateSegment;
using TrainDude.Features.Segments.SetCourse;
using TrainDude.Features.Settings.SetNameMode;
using TrainDude.Features.Stations.AddAxle;
using TrainDude.Features.Stations.CreateStation;
using TrainDude.Features.Stations.SetLocation;
using TrainDude.Features.Trips.CreateTrip;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropCommand), nameof(DropCommand))]
[JsonDerivedType(typeof(CreateLineCommand), nameof(CreateLineCommand))]
[JsonDerivedType(typeof(AppendSegmentCommand), nameof(AppendSegmentCommand))]
[JsonDerivedType(typeof(AssignTripCommand), nameof(AssignTripCommand))]
[JsonDerivedType(typeof(CreateSegmentCommand), nameof(CreateSegmentCommand))]
[JsonDerivedType(typeof(SetCourseCommand), nameof(SetCourseCommand))]
[JsonDerivedType(typeof(CreateStationCommand), nameof(Stations.CreateStation.CreateStationCommand))]
[JsonDerivedType(typeof(SetLocationCommand), nameof(SetLocationCommand))]
[JsonDerivedType(typeof(AddAxleCommand), nameof(AddAxleCommand))]
[JsonDerivedType(typeof(CreateTripCommand), nameof(Trips.CreateTrip.CreateTripCommand))]
[JsonDerivedType(typeof(SetNameModeCommand), nameof(SetNameModeCommand))]
public interface IDomainCommand<TResult>
    where TResult : BaseCommandResponse
{
}