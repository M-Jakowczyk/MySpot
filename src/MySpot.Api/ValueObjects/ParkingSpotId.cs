using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
    public sealed record ParkingSpotId
    {
        Guid Value { get; }
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