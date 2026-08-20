// <copyright file="GetLineQueryDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Lines;

using FluentValidation;

using LiteDB;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.Lines;

public sealed class GetLineQueryDataValidator
    : AbstractReadDataValidator<GetLineQuery>
{
    public GetLineQueryDataValidator(ILiteCollection<Line> lineRepository)
    {
        this.RuleFor(query => query.Id)
            .Must(id => lineRepository.Exists(x => x.Id == id))
            .WithMessage("There is no line with this ID.");
    }
}