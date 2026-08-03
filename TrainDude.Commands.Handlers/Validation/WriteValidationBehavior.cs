// <copyright file="WriteValidationBehavior.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Validation;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using Mediator;

public sealed class WriteValidationBehavior<TMessage, TResponse>
    : MessagePreProcessor<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly IValidator<TMessage>[] validators;

    public WriteValidationBehavior(IEnumerable<IValidator<TMessage>> validators)
    {
        this.validators = validators.ToArray();
    }

    protected override async ValueTask Handle(TMessage message, CancellationToken cancellationToken)
    {
        if (this.validators.Length > 0)
        {
            var context = new ValidationContext<TMessage>(message);

            var validationFailures = await Task.WhenAll(this.validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}