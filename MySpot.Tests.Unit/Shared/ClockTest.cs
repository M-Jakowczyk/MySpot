using MySpot.Application.Services;
using MySpot.Core.Abstractions;

namespace MySpot.Tests.Unit.Shared
{
    public class ClockTest : IClock
    {
        public DateTime Current() => new DateTime(2025, 01, 08);
    }
}