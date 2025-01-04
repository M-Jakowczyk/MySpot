using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
    public sealed record EmployeeName(string Value)
    {
        public string Value {get;} = Value ?? throw new InvalidEmployeeNameException(Value);

        public static implicit operator EmployeeName(string value) => new(value);
        public static implicit operator string(EmployeeName name) => name.Value;

    }
}