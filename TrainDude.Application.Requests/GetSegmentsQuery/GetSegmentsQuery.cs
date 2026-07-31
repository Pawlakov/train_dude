// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentsQuery;

using MediatR;

using TrainDude.Application.Requests.Base;

public class GetSegmentsQuery
    : BaseClientRequest, IRequest<GetSegmentsQueryResult>
{
}