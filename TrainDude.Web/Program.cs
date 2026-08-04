// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Data.HostBuilders;
using TrainDude.Commands.Handlers.HostBuilders;
using TrainDude.Queries.Data.HostBuilders;
using TrainDude.Queries.Handlers.HostBuilders;
using TrainDude.Web.Components;
using TrainDude.Web.HostBuilders;

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
            .AddReadDataServices(readConnectionString, isDevelopment)
            .AddWriteDataServices(writeConnectionString, isDevelopment)
            .AddReadDataValidation()
            .AddWriteDataValidation()
            .AddHandlers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");

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