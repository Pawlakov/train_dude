namespace TrainDude.Admin;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TrainDude.Admin.Commands;
using TrainDude.Admin.Services;
using TrainDude.Data.Extensions;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDataServices();
        builder.Services.AddSingleton<SeedService>();
        builder.Services.AddMediatR(config => { config.RegisterServicesFromAssemblyContaining<SeedCommand>(); });
        builder.Services.AddHostedService<InteractionService>();

        var host = builder.Build();
        await host.RunAsync();
    }
}