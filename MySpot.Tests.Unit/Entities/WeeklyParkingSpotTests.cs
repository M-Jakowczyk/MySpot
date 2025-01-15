using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Entities
{
    public class WeeklyParkingSpotTests
    {
        [Theory]
        [InlineData("2025-01-01")]
        [InlineData("2025-01-15")]
        public void given_invalid_date_add_reservation_should_throw_exception(string dateString)
        {
            // ARRANGE
            var invalidDate = DateTime.Parse(dateString);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, new Date(invalidDate), "John Doe", "XYZ123");

            // ACT
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_now)));

            // ASSERT
            exception.ShouldNotBeNull();                                     // Assert.NotNull(exception);
            exception.ShouldBeOfType<InvalidReservationDateException>();     // Assert.IsType<InvalidReservationDateException>(exception);
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_throw_exception()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, reservationDate, "John Doe", "XYZ123");
            _weeklyParkingSpot.AddReservation(reservation, _now);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_not_taken_date_add_reservation_should_throw_exception()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, reservationDate, "John Doe", "XYZ123");
            _weeklyParkingSpot.AddReservation(reservation, _now);

            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }

        #region Arrange

        private readonly Date _now;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;

        public WeeklyParkingSpotTests()
        {
            _now = new Date(new DateTime(2025, 01, 06));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
        }

        #endregion

    }
}
