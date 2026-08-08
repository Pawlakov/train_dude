// <copyright file="GetLineQueryDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetLineQuery;

using FluentValidation;

using LiteDB;

using TrainDude.Queries.Data.Aggregates;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.GetLineQuery;

public class GetLineQueryDataValidator
    : AbstractReadDataValidator<GetLineQuery>
{
    public GetLineQueryDataValidator(ILiteCollection<Line> lineRepository)
    {
        this.RuleFor(query => query.LineId)
            .Must(id => lineRepository.Exists(x => x.LineId == id))
            .WithMessage("There is not line with this ID.");
    }
}