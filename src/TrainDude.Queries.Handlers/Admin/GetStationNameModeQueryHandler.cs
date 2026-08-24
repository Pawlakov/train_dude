// <copyright file="GetStationNameModeQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Admin;

using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Contracts.Admin;
using TrainDude.Shared.Values;

public sealed class GetStationNameModeQueryHandler
    : IQueryHandler<GetStationNameModeQuery, GetStationNameModeQueryResult>
{
    private readonly ILiteCollection<Settings> settingsRepository;

    public GetStationNameModeQueryHandler(ILiteCollection<Settings> settingsRepository)
    {
        this.settingsRepository = settingsRepository;
    }

    public ValueTask<GetStationNameModeQueryResult> Handle(GetStationNameModeQuery query, CancellationToken cancellationToken)
    {
        var queryResult = this.settingsRepository.Query().FirstOrDefault();
        if (queryResult == null)
        {
            var result = new GetStationNameModeQueryResult
            {
                Mode = StationNameMode.Modern,
            };

            return ValueTask.FromResult(result);
        }
        else
        {
            var result = new GetStationNameModeQueryResult
            {
                Mode = queryResult.StationNameMode,
            };

            return ValueTask.FromResult(result);
        }
    }
}