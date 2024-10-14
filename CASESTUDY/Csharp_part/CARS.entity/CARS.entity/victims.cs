using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.entity
{
    public class Victim
    {
        
        private int victimID;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private char gender;
        private string contactInfo;

        
        public Victim() { }

        
        public Victim(int victimID, string firstName, string lastName, DateTime dateOfBirth, char gender, string contactInfo)
        {
            this.victimID = victimID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.contactInfo = contactInfo;
        }

        
        public int VictimID { get => victimID; set => victimID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public char Gender { get => gender; set => gender = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
    }
}
