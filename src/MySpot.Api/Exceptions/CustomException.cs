using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpot.Api.Exceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
            
        }
    }
}