using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Commands
{
    public record DeleteReservation(Guid ReservationId);
}