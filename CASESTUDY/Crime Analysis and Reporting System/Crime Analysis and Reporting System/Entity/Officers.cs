using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crime_Analysis_and_Reporting_System.Entity
{
    public class Officer
    {
        
        private int officerID;
        private string firstName;
        private string lastName;
        private string badgeNumber;
        private string rank;
        private string contactInfo;
        private int agencyID;

        
        public Officer() { }

        
        public Officer(int officerID, string firstName, string lastName, string badgeNumber, string rank, string contactInfo, int agencyID)
        {
            this.officerID = officerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.badgeNumber = badgeNumber;
            this.rank = rank;
            this.contactInfo = contactInfo;
            this.agencyID = agencyID;
        }

        
        public int OfficerID { get => officerID; set => officerID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string BadgeNumber { get => badgeNumber; set => badgeNumber = value; }
        public string Rank { get => rank; set => rank = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
        public int AgencyID { get => agencyID; set => agencyID = value; }
    }
}
