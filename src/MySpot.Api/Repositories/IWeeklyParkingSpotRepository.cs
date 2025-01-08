using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        IEnumerable<WeeklyParkingSpot> GetAll();
        WeeklyParkingSpot Get(ParkingSpotId id);
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);
        
    }
}