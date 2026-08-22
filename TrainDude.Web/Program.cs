// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Endpoints.HostBuilders;
using TrainDude.Domain.Exceptions;
using TrainDude.Integration.Events.Admin;
using TrainDude.Integration.Projections.Trips;
using TrainDude.Queries.Handlers.HostBuilders;
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
        var readConnectionString = builder.Configuration.GetConnectionString("Read");

        var writeConnectionString = builder.Configuration.GetConnectionString("Write");

        builder.Services
            .AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services
            .AddControllers();

        builder.Services
            .AddProblemDetails();

        builder.Services
            .AddReadDataServices(readConnectionString)
            .AddWriteServices(writeConnectionString, isDevelopment)
            .AddReadDataValidation()
            .AddRequestHandlers()
            .AddReadExceptionHandlers();

        builder.Host
            .UseWriteServices();

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

        app.MapControllers();
        app.MapWolverineEndpoints(opts => { opts.UseFluentValidationProblemDetailMiddleware(); });

        app.Run();
    }
}