// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetRadiiQuery;

using MediatR;

using TrainDude.Application.Requests.Base;

public class GetRadiiQuery
    : BaseClientRequest, IRequest<GetRadiiQueryResult>
{
}