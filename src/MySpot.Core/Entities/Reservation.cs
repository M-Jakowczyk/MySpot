using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class Reservation : ICloneable
    {
        public ReservationId Id { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
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
            return MemberwiseClone();
        }
    }
}
