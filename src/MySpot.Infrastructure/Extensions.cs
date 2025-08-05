using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Time;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using MySpot.Core.Abstractions;
using MySpot.Infrastructure.Exceptions;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPostgres(configuration)
                // .AddSingleton<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>()
                .AddSingleton<IClock, Clock>()
                .AddSingleton<ExceptionMiddleware>();
            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
