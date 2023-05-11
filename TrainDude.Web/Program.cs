// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;
using TrainDude.Schedule.Extensions;

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
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb://localhost:27017"));
        builder.Services.AddSingleton<IMongoDatabase>(services => services.GetRequiredService<IMongoClient>().GetDatabase("train_dude"));
        builder.Services.AddNetworkServices();
        builder.Services.AddScheduleServices();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<GetStationsQuery>();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}