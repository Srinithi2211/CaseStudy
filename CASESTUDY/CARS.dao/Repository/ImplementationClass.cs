using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARS.dao.Services;
using CARS.entity;
using CARS.Util;
using CARS.exception;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;

namespace CARS.dao.Repository
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        private SqlConnection connection;

        public CrimeAnalysisServiceImpl()
        {
            connection = DBUtil.GetDBConnection();
        }



        public bool CreateIncident(Incident incident)
        {
            SqlConnection connection = null; // Local connection variable to manage scope
            try
            {
                string query = "INSERT INTO Incidents (IncidentID, IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID) " +
                               "VALUES (@IncidentID, @IncidentType, @IncidentDate, @Location, @Description, @Status, @VictimID, @SuspectID)";

                connection = DBUtil.GetDBConnection(); // Initialize connection
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Adding parameters with values from the Incident object
                    cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);  // User-provided IncidentID
                    cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                    cmd.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
                    cmd.Parameters.AddWithValue("@Location", incident.Location);
                    cmd.Parameters.AddWithValue("@Description", incident.Description);
                    cmd.Parameters.AddWithValue("@Status", incident.Status);
                    cmd.Parameters.AddWithValue("@VictimID", incident.VictimID);
                    cmd.Parameters.AddWithValue("@SuspectID", incident.SuspectID);

                    connection.Open();  // Open connection
                    int result = cmd.ExecuteNonQuery();  // Execute the query

                    return result > 0;  // Return true if insert was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create incident: {ex.Message}");
                return false;
            }
            finally
            {
                // Ensure the connection is closed properly
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();  // Explicitly close the connection
                }
            }
        }

        private bool IncidentExists(int incidentID)
        {
            string query = "SELECT COUNT(*) FROM Incidents WHERE IncidentID = @IncidentID";
            using (SqlConnection connection = DBUtil.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IncidentID", incidentID);
                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Returns true if the incident exists
                }
            }
        }

        public bool UpdateIncidentStatus(string status, int incidentID)
        {
            // Check if the incident exists
            if (!IncidentExists(incidentID))
            {
                throw new IncidentNumberNotFoundException($"Incident ID {incidentID} not found.");
            }

            // Update the incident status if it exists
            string query = "UPDATE Incidents SET Status = @Status WHERE IncidentID = @IncidentID";
            using (SqlConnection connection = DBUtil.GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@IncidentID", incidentID);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if the update was successful
                }
            }
        }



        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            List<Incident> incidents = new List<Incident>();

            try
            {
                // Ensure the connection is open
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM Incidents WHERE IncidentDate BETWEEN @start AND @end";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@start", startDate);
                cmd.Parameters.AddWithValue("@end", endDate);

                // Execute the query
                SqlDataReader reader = cmd.ExecuteReader();

                // Check if there are rows returned
                if (!reader.HasRows)
                {
                    throw new IncidentNumberNotFoundException("No incidents found in the given date range.");
                }

                // Process each result and add to the list
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

                // Close the reader after finishing the reading
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                // Ensure the connection is closed after the operation
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return incidents;
        }



        public List<Incident> SearchIncidents(string incidentType)
        {
            List<Incident> incidents = new List<Incident>();

            try
            {
                // Ensure the connection is open
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM Incidents WHERE IncidentType = @type";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@type", incidentType);

                // Execute the query and read the data
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

                // Close the reader
                reader.Close();
            }
            catch (System.Exception ex)
            {
                // Log error and handle accordingly
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return incidents;
        }

        public Report GenerateIncidentReport(Incident incident)
        {
            return new Report(1, incident.IncidentID, 1, DateTime.Now, "Report generated", "Finalized");
        }

        public bool CreateVictim(Victim victim)
        {
            try
            {
                // Ensure the connection is open
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "INSERT INTO Victims (FirstName, LastName, DateOfBirth, Gender, ContactInfo) VALUES (@firstName, @lastName, @dob, @gender, @contact)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@firstName", victim.FirstName);
                cmd.Parameters.AddWithValue("@lastName", victim.LastName);
                cmd.Parameters.AddWithValue("@dob", victim.DateOfBirth);
                cmd.Parameters.AddWithValue("@gender", victim.Gender);
                cmd.Parameters.AddWithValue("@contact", victim.ContactInfo);

                // Execute the query
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (System.Exception ex)
            {
                // Log error and handle accordingly
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                // Ensure the connection is closed
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
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