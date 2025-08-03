using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

public class ManagerReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Manager;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> allParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = allParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations <= 4;
    }
}