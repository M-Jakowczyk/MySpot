namespace MySpot.Api.Models
{
    public class Reservation : ICloneable
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string ParkingSpotName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime Date { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
