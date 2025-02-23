using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class EmptyLicensePlateException : CustomException
    {
        public EmptyLicensePlateException() : base("License plate is empty")
        {
        }
    }
}