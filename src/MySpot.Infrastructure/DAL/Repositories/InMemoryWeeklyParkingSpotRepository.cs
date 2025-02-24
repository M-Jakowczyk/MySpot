using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly IClock _clock;
        private List<WeeklyParkingSpot> _weeklyParkingSpots;

        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _clock = clock;
            _weeklyParkingSpots = new(new List<WeeklyParkingSpot>()
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
        });
        }
        public void Add(WeeklyParkingSpot weeklyParkingSpot)
            => _weeklyParkingSpots.Add(weeklyParkingSpot);

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
            => _weeklyParkingSpots.Remove(weeklyParkingSpot);

        public WeeklyParkingSpot Get(ParkingSpotId id)
            => _weeklyParkingSpots.FirstOrDefault(x => x.Id == id);

        public IEnumerable<WeeklyParkingSpot> GetAll()
            => _weeklyParkingSpots;

        public void Update(WeeklyParkingSpot weeklyParkingSpot)
        {

        }
    }
}