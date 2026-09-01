// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TrainDude.Features.Radii.CreateRadius;
using TrainDude.Infrastructure.Admin;
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
            .AddWriteServices(writeConnectionString!, isDevelopment)
            .AddReadDataValidation()
            .AddRequestHandlers()
            .AddReadExceptionHandlers();

        builder.Host.UseWolverine(opts =>
        {
            opts.ApplicationAssembly = typeof(Program).Assembly;
            opts.Discovery.IncludeAssembly(typeof(DroppedProjectionHandler).Assembly);
            opts.Discovery.IncludeAssembly(typeof(DropEndpoint).Assembly);
            opts.Discovery.IncludeAssembly(typeof(CreateRadiusEndpoint).Assembly);

            opts.DescribeHandlerMatch(typeof(DropEndpoint));

            opts.Policies.AutoApplyTransactions();
            opts.Policies.UseDurableLocalQueues();
            opts.Policies.OnException<DomainException>().MoveToErrorQueue();

            opts.UseFluentValidation();

            // TODO some day we will do it this way
            // opts.PublishMessage<TripCreatedIntegrationEvent>().ToRabbitQueue("train-dude-projection").UseDurableInbox();
        });

        var app = builder.Build();

        app.UseExceptionHandler("/Error");

        if (isDevelopment)
        {
            app.UseWebAssemblyDebugging();
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