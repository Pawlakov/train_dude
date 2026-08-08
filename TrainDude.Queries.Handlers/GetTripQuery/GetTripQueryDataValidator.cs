// <copyright file="GetTripQueryDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetTripQuery;

using FluentValidation;

using LiteDB;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.GetTripQuery;

public class GetTripQueryDataValidator
    : AbstractReadDataValidator<GetTripQuery>
{
    public GetTripQueryDataValidator(ILiteCollection<Trip> tripRepository)
    {
        this.RuleFor(query => query.TripId)
            .Must(id => tripRepository.Exists(x => x.TripId == id))
            .WithMessage("There is no trip with this ID.");
    }
}