using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record ParkingSpotName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidParkingSpotNameException(Value);

        public static implicit operator ParkingSpotName(string value) => new(value);
        public static implicit operator string(ParkingSpotName name) => name.Value;

    }
}