namespace MySpot.Application.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ReservationId, Guid ParkingSpotId, DateTime Date, string EmployeeName, string LicensePlate);

}