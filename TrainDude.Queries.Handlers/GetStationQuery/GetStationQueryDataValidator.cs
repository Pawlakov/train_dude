// <copyright file="GetStationQueryDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetStationQuery;

using FluentValidation;

using LiteDB;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.GetStationQuery;

public class GetStationQueryDataValidator
    : AbstractReadDataValidator<GetStationQuery>
{
    public GetStationQueryDataValidator(ILiteCollection<Station> stationRepository)
    {
        this.RuleFor(query => query.StationId)
            .Must(id => stationRepository.Exists(x => x.StationId == id))
            .WithMessage("There is no station with this ID.");
    }
}