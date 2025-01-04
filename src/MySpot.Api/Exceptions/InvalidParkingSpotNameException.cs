namespace MySpot.Api.Exceptions
{
    public sealed class InvalidParkingSpotNameException : CustomException
    {
        public string Name { get; }
        public InvalidParkingSpotNameException(string name) 
            : base($"Parking spot name: {name} is invalide")
        {
            Name = name;
        }
    }
}