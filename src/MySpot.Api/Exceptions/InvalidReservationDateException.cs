using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Exceptions
{
    public sealed class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) 
            : base($"Reservation date: {date:d} is invalid.")
        {
            Date = date;
        }
    }
}