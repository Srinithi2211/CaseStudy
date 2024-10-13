using Crime_Analysis_and_Reporting_System.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crime_Analysis_and_Reporting_System.Util;
using Crime_Analysis_and_Reporting_System.Exceptions;

namespace Crime_Analysis_and_Reporting_System.BusinessLayer
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        private SqlConnection connection;

        public CrimeAnalysisServiceImpl()
        {
            connection = DBUtil.getDBConnection();
        }

        public bool CreateIncident(Incident incident)
        {
            string query = "INSERT INTO Incidents (IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID) VALUES (@type, @date, @location, @description, @status, @victimID, @suspectID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@type", incident.IncidentType);
            cmd.Parameters.AddWithValue("@date", incident.IncidentDate);
            cmd.Parameters.AddWithValue("@location", incident.Location);
            cmd.Parameters.AddWithValue("@description", incident.Description);
            cmd.Parameters.AddWithValue("@status", incident.Status);
            cmd.Parameters.AddWithValue("@victimID", incident.VictimID);
            cmd.Parameters.AddWithValue("@suspectID", incident.SuspectID);
            return cmd.ExecuteNonQuery() > 0;
        }

       
        public bool UpdateIncidentStatus(string status, int incidentID)
        {
            string query = "UPDATE Incidents SET Status = @status WHERE IncidentID = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@id", incidentID);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new IncidentNumberNotFoundException($"Incident with ID {incidentID} not found.");
            }
            return rowsAffected > 0;
        }

       
        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            List<Incident> incidents = new List<Incident>();
            string query = "SELECT * FROM Incidents WHERE IncidentDate BETWEEN @start AND @end";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@start", startDate);
            cmd.Parameters.AddWithValue("@end", endDate);
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                throw new IncidentNumberNotFoundException("No incidents found in the given date range.");
            }

            while (reader.Read())
            {
                incidents.Add(new Incident(
                    (int)reader["IncidentID"],
                    reader["IncidentType"].ToString(),
                    (DateTime)reader["IncidentDate"],
                    reader["Location"].ToString(),
                    reader["Description"].ToString(),
                    reader["Status"].ToString(),
                    (int)reader["VictimID"],
                    (int)reader["SuspectID"]));
            }
            return incidents;
        }


        public List<Incident> SearchIncidents(string incidentType)
        {
            List<Incident> incidents = new List<Incident>();
            string query = "SELECT * FROM Incidents WHERE IncidentType = @type";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@type", incidentType);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                incidents.Add(new Incident(
                    (int)reader["IncidentID"],
                    reader["IncidentType"].ToString(),
                    (DateTime)reader["IncidentDate"],
                    reader["Location"].ToString(),
                    reader["Description"].ToString(),
                    reader["Status"].ToString(),
                    (int)reader["VictimID"],
                    (int)reader["SuspectID"]));
            }
            return incidents;
        }

        public Report GenerateIncidentReport(Incident incident)
        {
            return new Report(1, incident.IncidentID, 1, DateTime.Now, "Report generated", "Finalized");
        }

        public bool CreateVictim(Victim victim)
        {
            string query = "INSERT INTO Victims (FirstName, LastName, DateOfBirth, Gender, ContactInfo) VALUES (@firstName, @lastName, @dob, @gender, @contact)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@firstName", victim.FirstName);
            cmd.Parameters.AddWithValue("@lastName", victim.LastName);
            cmd.Parameters.AddWithValue("@dob", victim.DateOfBirth);
            cmd.Parameters.AddWithValue("@gender", victim.Gender);
            cmd.Parameters.AddWithValue("@contact", victim.ContactInfo);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CreateSuspect(Suspect suspect)
        {
            string query = "INSERT INTO Suspects (FirstName, LastName, DateOfBirth, Gender, ContactInfo) VALUES (@firstName, @lastName, @dob, @gender, @contact)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@firstName", suspect.FirstName);
            cmd.Parameters.AddWithValue("@lastName", suspect.LastName);
            cmd.Parameters.AddWithValue("@dob", suspect.DateOfBirth);
            cmd.Parameters.AddWithValue("@gender", suspect.Gender);
            cmd.Parameters.AddWithValue("@contact", suspect.ContactInfo);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CreateOfficer(Officer officer)
        {
            string query = "INSERT INTO Officers (FirstName, LastName, BadgeNumber, Rank, ContactInfo, AgencyID) VALUES (@firstName, @lastName, @badge, @rank, @contact, @agencyID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@firstName", officer.FirstName);
            cmd.Parameters.AddWithValue("@lastName", officer.LastName);
            cmd.Parameters.AddWithValue("@badge", officer.BadgeNumber);
            cmd.Parameters.AddWithValue("@rank", officer.Rank);
            cmd.Parameters.AddWithValue("@contact", officer.ContactInfo);
            cmd.Parameters.AddWithValue("@agencyID", officer.AgencyID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CreateLawEnforcementAgency(LawEnforcementAgency agency)
        {
            string query = "INSERT INTO LawEnforcementAgencies (AgencyName, Jurisdiction, ContactInfo) VALUES (@name, @jurisdiction, @contact)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", agency.AgencyName);
            cmd.Parameters.AddWithValue("@jurisdiction", agency.Jurisdiction);
            cmd.Parameters.AddWithValue("@contact", agency.ContactInfo);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CreateEvidence(Evidence evidence)
        {
            string query = "INSERT INTO Evidence (Description, LocationFound, IncidentID) VALUES (@description, @location, @incidentID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@description", evidence.Description);
            cmd.Parameters.AddWithValue("@location", evidence.LocationFound);
            cmd.Parameters.AddWithValue("@incidentID", evidence.IncidentID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CreateReport(Report report)
        {
            string query = "INSERT INTO Reports (IncidentID, ReportingOfficer, ReportDate, ReportDetails, Status) VALUES (@incidentID, @officerID, @date, @details, @status)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@incidentID", report.IncidentID);
            cmd.Parameters.AddWithValue("@officerID", report.ReportingOfficerID);
            cmd.Parameters.AddWithValue("@date", report.ReportDate);
            cmd.Parameters.AddWithValue("@details", report.ReportDetails);
            cmd.Parameters.AddWithValue("@status", report.Status);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
    
}
