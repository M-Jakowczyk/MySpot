using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Tests.Unit.Shared;
using Shouldly;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        var weeklyParkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
        var command = new CreateReservation(Guid.NewGuid(), weeklyParkingSpot.Id,
            _clock.Current().AddMinutes(5), "John Doe", "XYZ123");

        var reservationId = await _reservationService.CreateAsync(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    #region Arrange

    private static readonly IClock _clock = new ClockTest();
    private readonly IReservationService _reservationService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationServiceTests()
    {
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}