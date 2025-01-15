using MySpot.Application.Services;

namespace MySpot.Tests.Unit.Shared
{
    public class ClockTest : IClock
    {
        public DateTime Current() => new DateTime(2025, 01, 08);
    }
}