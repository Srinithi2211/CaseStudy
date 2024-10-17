using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.entity
{
    public class Evidence
    {
        
        private int evidenceID;
        private string description;
        private string locationFound;
        private int incidentID;

        
        public Evidence() { }

        
        public Evidence(int evidenceID, string description, string locationFound, int incidentID)
        {
            this.evidenceID = evidenceID;
            this.description = description;
            this.locationFound = locationFound;
            this.incidentID = incidentID;
        }

        
        public int EvidenceID { get => evidenceID; set => evidenceID = value; }
        public string Description { get => description; set => description = value; }
        public string LocationFound { get => locationFound; set => locationFound = value; }
        public int IncidentID { get => incidentID; set => incidentID = value; }
    }
}
