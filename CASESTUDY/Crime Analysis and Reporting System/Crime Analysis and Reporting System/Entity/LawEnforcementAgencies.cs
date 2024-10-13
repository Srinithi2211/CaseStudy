using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Entity
{
    public class LawEnforcementAgency
    {
        
        private int agencyID;
        private string agencyName;
        private string jurisdiction;
        private string contactInfo;
        private string officers;

        
        public LawEnforcementAgency() { }

        
        public LawEnforcementAgency(int agencyID, string agencyName, string jurisdiction, string contactInfo, string officers)
        {
            this.agencyID = agencyID;
            this.agencyName = agencyName;
            this.jurisdiction = jurisdiction;
            this.contactInfo = contactInfo;
            this.officers = officers;
        }

        
        public int AgencyID { get => agencyID; set => agencyID = value; }
        public string AgencyName { get => agencyName; set => agencyName = value; }
        public string Jurisdiction { get => jurisdiction; set => jurisdiction = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
        public string Officers { get => officers; set => officers = value; }
    }
}
