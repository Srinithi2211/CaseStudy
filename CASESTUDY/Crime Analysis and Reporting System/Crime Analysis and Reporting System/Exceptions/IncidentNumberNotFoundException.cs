using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Exceptions
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
