﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal static class Extansions
    {
        private const string SectionName = "postgres";
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            services.Configure<PostgresOptions>(section);
            var options = configuration.GetOptions<PostgresOptions>(SectionName);

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddHostedService<DatabaseInitializer>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
