using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ReservationId, Guid ParkingSpotId, int Capacity,
        DateTime Date, string EmployeeName, string LicensePlate);

}