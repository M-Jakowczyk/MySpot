using MySpot.Core;
using MySpot.Application;
using MySpot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddCore()
    .AddInfrastrucure()
    .AddControllers();

var app = builder.Build();


app.MapControllers();
app.Run();
