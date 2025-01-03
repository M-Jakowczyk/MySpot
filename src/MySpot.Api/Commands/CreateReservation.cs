using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Commands
{
    public record CreateReservation( Guid ReservationId, Guid ParkingSpotId, DateTime Date, string EmployeeName, string LicensePlate);
    
}