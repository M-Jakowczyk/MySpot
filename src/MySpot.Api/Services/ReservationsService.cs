using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;
       
        public ReservationsService(List<WeeklyParkingSpot> weeklyParkingSpot)
        {
            _weeklyParkingSpots = weeklyParkingSpot;
        }

        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(r => r.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly()
            => _weeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id= x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    Date = x.Date.Value.Date,
                    EmployeeName = x.EmployeeName,
                });

        public Guid? Create(CreateReservation command){
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id.Equals(command.ParkingSpotId));
            if (weeklyParkingSpot == null)
            {
                return default;
            }
            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, 
                (DateTimeOffset)command.Date, command.EmployeeName, command.LicensePlate);
            weeklyParkingSpot.AddReservation(reservation, Date.Now);
            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command){
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null){
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id.Equals(command.ReservationId));
            if(existingReservation is null)
            {
                return false;
            }

            if(existingReservation.Date <= Date.Now)
            {
                return false;
            }
            
            existingReservation.ChangeLicensePlate(command.LicensePlate);
            return true;
        }

        public bool Delete(DeleteReservation command){
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null){
                return false;
            }
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id.Equals(command.ReservationId));
            if(existingReservation is null)
            {
                return false;
            }
            weeklyParkingSpot.RemoveReservation(existingReservation);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) 
            => _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(y => y.Id.Equals(reservationId)));
    }
}