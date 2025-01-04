using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
    public record LicensePlate
    {
        public string Value { get; }
        public LicensePlate(string value)
        {
            if(string.IsNullOrWhiteSpace(value)) 
            {
                throw new EmptyLicensePlateException();
            }
            if(value.Length is < 5 or > 8)
            {
                throw new InvalidLicensePlateException(value);
            }
            Value = value;
        }

        public static implicit operator string(LicensePlate licensePlate) => licensePlate?.Value;
        public static implicit operator LicensePlate(string licensePlate) => new LicensePlate(licensePlate);
    }
}