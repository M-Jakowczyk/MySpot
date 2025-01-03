using MySpot.Api.Exceptions;

namespace MySpot.Api.Models
{
    public class Reservation : ICloneable
    {
        public Guid Id { get; }
        public Guid ParkingSpotId { get; }
        public string EmployeeName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime Date { get; private set; }

        public Reservation(Guid id, Guid parkingSpotId, DateTime date, string employeeName, string licensePlate)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicensePlate(licensePlate);
            Date = date;
        }

        public void ChangeLicensePlate(string licensePlate)
        {
            if(string.IsNullOrWhiteSpace(licensePlate)) 
            {
                throw new EmptyLicensePlateException();
            }
            LicensePlate = licensePlate;
        }
       
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
