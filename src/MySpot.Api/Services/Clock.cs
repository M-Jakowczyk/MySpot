using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Services
{
    public class Clock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}