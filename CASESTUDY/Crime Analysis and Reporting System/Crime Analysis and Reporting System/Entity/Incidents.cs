using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Entity
{
    public class Incident
    {
        
        private int incidentID;
        private string incidentType;
        private DateTime incidentDate;
        private string location;
        private string description;
        private string status;
        private int victimID;
        private int suspectID;

        
        public Incident() { }

        
        public Incident(int incidentID, string incidentType, DateTime incidentDate, string location, string description, string status, int victimID, int suspectID)
        {
            this.incidentID = incidentID;
            this.incidentType = incidentType;
            this.incidentDate = incidentDate;
            this.location = location;
            this.description = description;
            this.status = status;
            this.victimID = victimID;
            this.suspectID = suspectID;
        }

        
        public int IncidentID { get => incidentID; set => incidentID = value; }
        public string IncidentType { get => incidentType; set => incidentType = value; }
        public DateTime IncidentDate { get => incidentDate; set => incidentDate = value; }
        public string Location { get => location; set => location = value; }
        public string Description { get => description; set => description = value; }
        public string Status { get => status; set => status = value; }
        public int VictimID { get => victimID; set => victimID = value; }
        public int SuspectID { get => suspectID; set => suspectID = value; }
    }
}
