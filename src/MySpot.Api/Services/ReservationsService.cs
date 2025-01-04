using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Models;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static Clock _clock = new();
        private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
        {   
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), _clock.Current(),  _clock.Current().AddDays(7), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), _clock.Current(),  _clock.Current().AddDays(7), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), _clock.Current(),  _clock.Current().AddDays(7), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), _clock.Current(),  _clock.Current().AddDays(7), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), _clock.Current(),  _clock.Current().AddDays(7), "P5")
        };

        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(r => r.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly()
            => WeeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id= x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    Date = x.Date,
                    EmployeeName = x.EmployeeName,
                });

        public Guid? Create(CreateReservation command){
            var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
            if (weeklyParkingSpot == null)
            {
                return default;
            }
            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, 
                command.Date, command.EmployeeName, command.LicensePlate);
            weeklyParkingSpot.AddReservation(reservation, _clock.Current());
            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command){
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null){
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
            if(existingReservation is null)
            {
                return false;
            }

            if(existingReservation.Date <= _clock.Current())
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
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
            if(existingReservation is null)
            {
                return false;
            }
            weeklyParkingSpot.RemoveReservation(existingReservation);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) 
            => WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(y => y.Id == reservationId));
    }
}