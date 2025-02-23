using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record ParkingSpotId
    {
        public Guid Value { get; }
        public ParkingSpotId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidGuidException();
            }
            Value = value;
        }
        public static implicit operator ParkingSpotId(Guid value) => new(value);
        public static implicit operator Guid(ParkingSpotId data) => data.Value;
    }
}