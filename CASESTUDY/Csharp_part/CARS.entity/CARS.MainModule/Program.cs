using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARS.entity;
using CARS.exception;
using CARS.Util;
using CARS.dao;
using CARS.dao.Services;
using CARS.dao.Repository;

namespace CARS.MainModule
{
    class MainModule
    {
        static void Main(string[] args)
        {
            // Create the Crime Analysis Service instance
            ICrimeAnalysisService crimeService = new CrimeAnalysisServiceImpl();

            try
            {
                // Example: Create a new incident
                Incident incident = new Incident
                {
                    IncidentType = "Robbery",
                    IncidentDate = DateTime.Now,
                    Location = "123 Main St",
                    Description = "A robbery occurred at the address",
                    Status = "Open",
                    VictimID = 1,
                    SuspectID = 2
                };

                bool isIncidentCreated = crimeService.CreateIncident(incident);
                Console.WriteLine(isIncidentCreated ? "Incident created successfully!" : "Failed to create incident.");

                // Example: Update incident status
                bool isStatusUpdated = crimeService.UpdateIncidentStatus("Closed", incident.IncidentID);
                Console.WriteLine(isStatusUpdated ? "Incident status updated successfully!" : "Failed to update status.");

                // Example: Get incidents within a date range
                List<Incident> incidentsInDateRange = crimeService.GetIncidentsInDateRange(DateTime.Now.AddMonths(-1), DateTime.Now);
                foreach (Incident inc in incidentsInDateRange)
                {
                    Console.WriteLine($"Incident ID: {inc.IncidentID}, Type: {inc.IncidentType}, Status: {inc.Status}");
                }

                // Example: Search for incidents by type
                List<Incident> robberyIncidents = crimeService.SearchIncidents("Robbery");
                foreach (Incident rob in robberyIncidents)
                {
                    Console.WriteLine($"Robbery Incident ID: {rob.IncidentID}, Status: {rob.Status}");
                }

                // Example: Create a new victim
                Victim victim = new Victim
                {
                    FirstName = "Sathya",
                    LastName = "Priya",
                    DateOfBirth = new DateTime(2024, 5, 21),
                    Gender = 'F',
                    ContactInfo = "123-456-7890"
                };

                bool isVictimCreated = crimeService.CreateVictim(victim);
                Console.WriteLine(isVictimCreated ? "Victim created successfully!" : "Failed to create victim.");

                // Example: Generate a report for the incident
                Report report = crimeService.GenerateIncidentReport(incident);
                Console.WriteLine($"Generated report with ID: {report.ReportID}, Status: {report.Status}");

            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("Crime Analysis and Reporting System ended.");
        }
    }
}
