// <copyright file="ClientHostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.HostBuilders;

using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using TrainDude.Application.Requests.Validation;

public static class ClientHostBuilderExtensions
{
    public static IServiceCollection AddInputValidation(this IServiceCollection services)
    {
        var inputValidatorInterfaceType = typeof(IInputValidator<>);
        var list = inputValidatorInterfaceType.Assembly.GetTypes()
            .Where(mytype => mytype.GetInterface(inputValidatorInterfaceType.Name) != null && !mytype.IsInterface && !mytype.IsAbstract)
            .ToList();

        foreach (var item in list)
        {
            var interfaceType = item.GetInterface(inputValidatorInterfaceType.Name);

            services.TryAddEnumerable(new ServiceDescriptor(interfaceType!, item, ServiceLifetime.Scoped));
            services.TryAdd(new ServiceDescriptor(item, item, ServiceLifetime.Scoped));
        }

        return services;
    }
}