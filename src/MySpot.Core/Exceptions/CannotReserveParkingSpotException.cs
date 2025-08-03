using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;

public class CannotReserveParkingSpotException(ParkingSpotId parkingSpotId)
    : CustomException($"Cannot reserve parking spot with ID: {parkingSpotId}.")
{
    private readonly ParkingSpotId _parkingSpotId = parkingSpotId;
}