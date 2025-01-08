using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Services
{
    public class Clock : IClock
    {
        public DateTime Current() => DateTime.Now;
    }
}