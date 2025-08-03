using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

public class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private IClock _clock;
    
    public RegularEmployeeReservationPolicy(IClock clock) 
        => _clock = clock;
    
    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Employee;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> allParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = allParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations < 2 && _clock.Current().Hour < 4;
    }
}