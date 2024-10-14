using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.exception
{
    public class IncidentNumberNotFoundException : Exception
    {
        public IncidentNumberNotFoundException() : base("Incident number not found.")
        {
        }

        public IncidentNumberNotFoundException(string message) : base(message)
        {
        }

        public IncidentNumberNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
