using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Models;
using MySpot.Api.Repositories;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
    public class ReservationsService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
       
        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        }

        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(r => r.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly()
            => _weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id= x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    Date = x.Date.Value.Date,
                    EmployeeName = x.EmployeeName,
                });

        public Guid? Create(CreateReservation command){
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(parkingSpotId);
            if (weeklyParkingSpot is null)
            {
                return default;
            }
            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, 
                new Date(command.Date), command.EmployeeName, command.LicensePlate);
            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));
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

            if(existingReservation.Date <= new Date(_clock.Current()))
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
            => _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(y => y.Id.Equals(reservationId)));
    }
}