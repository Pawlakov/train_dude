// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Mediator;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using TrainDude.Web.Client.HostBuilders;
using TrainDude.Web.Client.Seed;
using TrainDude.Web.Client.Services;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddScoped(serviceProvider => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
        });

        builder.Services.AddScoped<ISender, HttpMediator>();
        builder.Services.AddQueryInputValidation();
        builder.Services.AddCommandInputValidation();

        builder.Services.AddScoped<SeedService>();
        builder.Services.AddScoped<SeedLoader>();

        await builder.Build().RunAsync();
    }
}