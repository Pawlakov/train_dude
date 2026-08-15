// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.HostBuilders;

using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using TrainDude.Web.Client.Validation;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddQueryInputValidation(this IServiceCollection services)
    {
        var inputValidatorInterfaceType = typeof(IInputValidator<>);
        var list = typeof(HostBuilderExtensions).Assembly.GetTypes()
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

    public static IServiceCollection AddCommandInputValidation(this IServiceCollection services)
    {
        var inputValidatorInterfaceType = typeof(IInputValidator<>);
        var list = typeof(HostBuilderExtensions).Assembly.GetTypes()
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