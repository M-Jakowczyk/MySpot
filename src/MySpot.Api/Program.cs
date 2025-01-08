using MySpot.Api.Repositories;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IClock, Clock>()
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationService, ReservationsService>()
    .AddControllers();

var app = builder.Build();


app.MapControllers();
app.Run();
