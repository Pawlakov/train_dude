// <copyright file="Program.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web;

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TrainDude.Features.Lines.GetLines;
using TrainDude.Features.Radii.GetRadii;
using TrainDude.Features.Segments.GetSegments;
using TrainDude.Features.Settings.SetNamingPolicy;
using TrainDude.Features.Shared.Drop;
using TrainDude.Features.Shared.Exceptions;
using TrainDude.Features.Stations.GetStations;
using TrainDude.Features.Trips.GetTrips;
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
            .AddInteractiveWebAssemblyComponents()
            .AddAuthenticationStateSerialization();

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            })
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

        builder.Services
            .AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.AddPolicy(
                "SuperUser",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email, builder.Configuration["Authorization:SuperUser"]);
                });
            });

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.MapStaticAssets().Add(endpointBuilder => endpointBuilder.Metadata.Add(new AllowAnonymousAttribute()));

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapWolverineEndpoints(opts =>
        {
            opts.UseFluentValidationProblemDetailMiddleware();
            opts.RequireAuthorizeOnAll();
        });

        app.MapGet(
            "/account/login",
            (HttpContext httpContext, string? returnUrl) =>
            {
                var redirectUri = string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl;
                return Results.Challenge(new AuthenticationProperties { RedirectUri = redirectUri, }, [GoogleDefaults.AuthenticationScheme]);
            })
            .AllowAnonymous();

        app.MapGet(
            "/account/logout",
            async (HttpContext httpContext) =>
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/");
            })
            .AllowAnonymous();

        app.Run();
    }
}