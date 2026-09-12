// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using RestSharp;

using TrainDude.Web.Client.Features.Admin;
using TrainDude.Web.Client.HostBuilders;
using TrainDude.Web.Client.Services;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddAuthenticationStateDeserialization();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(
            "SuperUser",
            policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("SuperUser");
            });
        });

        var baseUrl = builder.HostEnvironment.BaseAddress;
        builder.Services.AddSingleton(serviceProvider => new HttpClient { BaseAddress = new Uri(baseUrl) });
        builder.Services.AddSingleton<IRestClient>(serviceProvider => new RestClient(baseUrl));

        builder.Services.AddSingleton<ApiClient>();
        builder.Services.AddSingleton<SeedService>();
        builder.Services.AddSingleton<SeedLoader>();
        builder.Services.AddSingleton<MapService>();

        builder.Services.AddInputValidation();

        await builder.Build().RunAsync();
    }
}