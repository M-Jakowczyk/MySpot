using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
    public sealed record ReservationId
    {
        Guid Value { get; }
        public ReservationId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidGuidException();
            }
            Value = value;
        }
        public static implicit operator ReservationId(Guid value) => new(value);
        public static implicit operator Guid(ReservationId data) => data.Value;
    }
}