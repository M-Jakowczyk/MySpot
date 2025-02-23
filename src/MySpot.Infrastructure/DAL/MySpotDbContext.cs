using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Infrastructure.DAL.Configuration;

namespace MySpot.Infrastructure.DAL
{
    public class MySpotDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

        public MySpotDbContext(DbContextOptions<MySpotDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            //modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            //modelBuilder.ApplyConfiguration(new WeeklyParkingSpotConfiuration());

            //new ReservationConfiguration().Configure(modelBuilder.Entity<Reservation>());
            //new WeeklyParkingSpotConfiuration().Configure(modelBuilder.Entity<WeeklyParkingSpot>());
        }
    }
}
