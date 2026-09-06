// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TrainDude.Features.Lines.Contracts.CreateLine;
using TrainDude.Features.Lines.CreateLine;
using TrainDude.Features.Lines.GetLines;
using TrainDude.Features.Radii.Contracts.CreateRadius;
using TrainDude.Features.Radii.CreateRadius;
using TrainDude.Features.Radii.GetRadii;
using TrainDude.Features.Segments.CreateSegment;
using TrainDude.Features.Segments.GetSegments;
using TrainDude.Features.Settings.GetNamingPolicy;
using TrainDude.Features.Settings.SetNamingPolicy;
using TrainDude.Features.Shared.Drop;
using TrainDude.Features.Shared.Exceptions;
using TrainDude.Features.Stations.CreateStation;
using TrainDude.Features.Stations.GetStations;
using TrainDude.Features.Trips.CreateTrip;
using TrainDude.Features.Trips.GetTrips;
using TrainDude.Infrastructure.Radii.Projections;
using TrainDude.Web.Components;
using TrainDude.Web.HostBuilders;

using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

/// <summary>
/// The main class.
/// </summary>
public static class Program
{
    /// <summary>
    /// The main function.
    /// </summary>
    /// <param name="args">CL arguments (unused).</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var isDevelopment = builder.Environment.IsDevelopment();

        var writeConnectionString = builder.Configuration.GetConnectionString("Write");

        builder.Services
            .AddLogging(logging => logging.AddConsole());

        builder.Services
            .AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services
            .AddControllers();

        builder.Services
            .AddProblemDetails();

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        builder.Services
            .AddWriteServices(writeConnectionString!, isDevelopment)
            .AddReadExceptionHandlers();

        builder.Host.UseWolverine(opts =>
        {
            opts.ApplicationAssembly = typeof(Program).Assembly;
            opts.Discovery.IncludeAssembly(typeof(GetLinesEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(GetRadiiEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(GetSegmentsEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(SetNamingPolicyEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(DropEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(GetStationsEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(GetTripsEndpoint).Assembly);

            opts.DescribeHandlerMatch(typeof(DropEndpoint));

            opts.Policies.AutoApplyTransactions();
            opts.Policies.UseDurableLocalQueues();
            opts.Policies.OnException<DomainException>().MoveToErrorQueue();

            opts.UseFluentValidation();
        });

        var app = builder.Build();

        app.UseExceptionHandler("/Error");

        if (isDevelopment)
        {
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapWolverineEndpoints(opts => { opts.UseFluentValidationProblemDetailMiddleware(); });

        app.Run();
    }
}