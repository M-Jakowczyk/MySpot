namespace MySpot.Core.Exceptions
{
    public sealed class InvalidLicensePlateException : CustomException
    {
        public string LicensePlate { get; }
        public InvalidLicensePlateException(string licensePlate) 
            : base($"License plate: {licensePlate} is invalide")
        {
            LicensePlate = licensePlate;
        }
    }
}