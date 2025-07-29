using MySpot.Application.Commands;
using MySpot.Application.DTOs;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
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

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservation = await GetAllWeeklyAsync();
            return reservation.SingleOrDefault(r => r.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            return weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    Date = x.Date.Value.Date,
                    EmployeeName = x.EmployeeName,
                });

        }

        public async Task<Guid?> CreateAsync(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAsync(parkingSpotId);
            if (weeklyParkingSpot is null)
            {
                return default;
            }
            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId,
                new Date(command.Date), command.EmployeeName, command.LicensePlate);
            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);

            return reservation.Id;
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }
            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
            if (existingReservation is null)
            {
                return false;
            }

            //if (existingReservation.Date <= new Date(_clock.Current()))
            //{
            //    return false;
            //}

            existingReservation.ChangeLicensePlate(command.LicensePlate);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);

            return true;
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id.Equals(command.ReservationId));
            if (existingReservation is null)
            {
                return false;
            }
            weeklyParkingSpot.RemoveReservation(existingReservation);
            await _weeklyParkingSpotRepository.DeleteAsync(weeklyParkingSpot);

            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(Guid reservationId)
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            return weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(y => y.Id == new ReservationId(reservationId)));

        }
    }
}