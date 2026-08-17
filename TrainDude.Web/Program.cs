// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using JasperFx;
using JasperFx.CodeGeneration;
using JasperFx.Events.Daemon;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Handlers.HostBuilders;
using TrainDude.Integration.Events.Admin;
using TrainDude.Integration.Events.Trips;
using TrainDude.Queries.Data.HostBuilders;
using TrainDude.Queries.Handlers.HostBuilders;
using TrainDude.Queries.Handlers.Projections.Trips;
using TrainDude.Web.Components;
using TrainDude.Web.HostBuilders;

using Wolverine;

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

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddControllers();

        var readConnectionString = builder.Configuration.GetConnectionString("Read");
        var writeConnectionString = builder.Configuration.GetConnectionString("Write");
        var isDevelopment = builder.Environment.IsDevelopment();
        builder.Services
            .AddReadDataServices(readConnectionString)
            .AddWriteDataServices(writeConnectionString, isDevelopment)
            .AddReadDataValidation()
            .AddWriteDataValidation()
            .AddRequestHandlers()
            .AddExceptionHandlers();

        builder.Host.UseWolverine(opts =>
        {
            opts.Policies.AutoApplyTransactions();

            opts.Discovery.IncludeAssembly(typeof(TripCreatedProjectionHandler).Assembly);

            opts.Policies.UseDurableLocalQueues();
            opts.PublishMessage<DroppedIntegrationEvent>().ToLocalQueue("train-dude-projection").UseDurableInbox();
            opts.PublishMessage<TripCreatedIntegrationEvent>().ToLocalQueue("train-dude-projection").UseDurableInbox();

            // TODO some day we will do it this way
            // opts.PublishMessage<TripCreatedIntegrationEvent>().ToRabbitQueue("train-dude-projection").UseDurableInbox();
        });

        var app = builder.Build();

        app.UseExceptionHandler("/Error");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
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

        app.MapControllers();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}