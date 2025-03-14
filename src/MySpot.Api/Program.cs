using MySpot.Core;
using MySpot.Application;
using MySpot.Infrastructure;
using MySpot.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddCore()
    .AddInfrastrucure(builder.Configuration)
    .AddControllers();

var app = builder.Build();
app.MapControllers();




app.Run();
