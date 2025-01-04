using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Models
{
    public class Reservation : ICloneable
    {
        public ReservationId Id { get; }
        public ParkingSpotId ParkingSpotId { get; }
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public Date Date { get; private set; }

        public Reservation(ReservationId id, ParkingSpotId parkingSpotId, Date date, EmployeeName employeeName, LicensePlate licensePlate)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicensePlate(licensePlate);
            Date = date;
        }

        public void ChangeLicensePlate(LicensePlate licensePlate)
            => LicensePlate = licensePlate;
       
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
