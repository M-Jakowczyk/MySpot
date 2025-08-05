using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record ReservationId
    {
        public Guid Value { get; }
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
        
        public static ReservationId Create() => new(Guid.NewGuid());
    }
}