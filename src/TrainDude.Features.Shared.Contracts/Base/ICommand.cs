// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

public interface ICommand<TResult>
    : IRequest<TResult>
    where TResult : IResult
{
}