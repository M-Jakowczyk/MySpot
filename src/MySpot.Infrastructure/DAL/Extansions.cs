﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL
{
    public static class Extansions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=MySpot;Username=postgres;password=superpassword";
            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }
    }
}
