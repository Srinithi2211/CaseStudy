using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Entity
{
    public class Report
    {
        
        private int reportID;
        private int incidentID;
        private int reportingOfficerID;
        private DateTime reportDate;
        private string reportDetails;
        private string status;

        
        public Report() { }

        
        public Report(int reportID, int incidentID, int reportingOfficerID, DateTime reportDate, string reportDetails, string status)
        {
            this.reportID = reportID;
            this.incidentID = incidentID;
            this.reportingOfficerID = reportingOfficerID;
            this.reportDate = reportDate;
            this.reportDetails = reportDetails;
            this.status = status;
        }

        
        public int ReportID { get => reportID; set => reportID = value; }
        public int IncidentID { get => incidentID; set => incidentID = value; }
        public int ReportingOfficerID { get => reportingOfficerID; set => reportingOfficerID = value; }
        public DateTime ReportDate { get => reportDate; set => reportDate = value; }
        public string ReportDetails { get => reportDetails; set => reportDetails = value; }
        public string Status { get => status; set => status = value; }
    }
}
