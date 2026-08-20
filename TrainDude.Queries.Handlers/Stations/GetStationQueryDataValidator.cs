// <copyright file="GetStationQueryDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Stations;

using FluentValidation;

using LiteDB;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.Stations;

public sealed class GetStationQueryDataValidator
    : AbstractReadDataValidator<GetStationQuery>
{
    public GetStationQueryDataValidator(ILiteCollection<Station> stationRepository)
    {
        this.RuleFor(query => query.Id)
            .Must(id => stationRepository.Exists(x => x.Id == id))
            .WithMessage("There is no station with this ID.");
    }
}