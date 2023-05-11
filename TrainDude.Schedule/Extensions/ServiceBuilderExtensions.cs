namespace TrainDude.Schedule.Extensions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using TrainDude.Schedule.Services;

public static class ServiceBuilderExtensions
{
    public static IServiceCollection AddScheduleServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<SeedService>();
    }
}