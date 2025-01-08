using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySpot.Api.Services;

namespace MySpot.Tests.Unit.Shared
{
    public class ClockTest : IClock
    {
        public DateTime Current() => new DateTime(2025,01,08);
    }
}