namespace MySpot.Application.Commands
{
    public record CreateReservation(Guid ReservationId, Guid ParkingSpotId, DateTime Date, string EmployeeName, string LicensePlate);

}