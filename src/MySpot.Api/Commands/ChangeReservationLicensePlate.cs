using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Commands
{
    public record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate);
}