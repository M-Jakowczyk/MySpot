using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Models;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<string> ParkingSpotName = new()
        { "P1", "P2","P3","P4","P5"};
        private static int _id =1;
        private static readonly List<Reservation> Reservations = new();

        public Reservation Get(int id)
            => Reservations.SingleOrDefault(r => r.Id == id);

        public IEnumerable<Reservation> GetAll()
            => Reservations;

        public int? Create(Reservation reservation){
            if(ParkingSpotName.All(x => x != reservation.ParkingSpotName))
            {
                return default;
            }

            reservation.Date = DateTime.UtcNow.AddDays(1).Date;

            var reservationAlreadyExists = Reservations.Any(x => 
                x.ParkingSpotName == reservation.ParkingSpotName &&
                x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExists)
            {
                return default;
            }

            reservation.Id = _id;
            Reservations.Add(reservation);
            ParkingSpotName.Any(x => x == reservation.ParkingSpotName);
            _id++;

            return reservation.Id;
        }

        public bool Update(int id, Reservation reservation){
            var reservationToUpdate = Reservations.SingleOrDefault(x => x.Id == id);
            if(reservationToUpdate is null){
                return false;
            }
            reservationToUpdate.LicensePlate = reservation.LicensePlate;
            return true;
        }

        public bool Delete(int id){
            var reservationToDelete = Reservations.SingleOrDefault(x => x.Id == id);
            if(reservationToDelete is null){
                return false;
            }
            Reservations.Remove(reservationToDelete);
            return true;
        }
    }
}