using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARS.entity;
using CARS.Util;
using CARS.dao.Repository;
using CARS.dao.Services;
using CARS.dao;
using CARS.exception;

namespace CARS.MainApp
{
    class MainModule
    {
        static void Main(string[] args)
        {
            // Create the Crime Analysis Service instance
            ICrimeAnalysisService crimeService = new CrimeAnalysisServiceImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=====CRIME ANALYSIS AND REPORTING SYSTEM=====");
                Console.WriteLine("1. Create Incident");
                Console.WriteLine("2. Update Incident Status");
                Console.WriteLine("3. Get Incidents in Date Range");
                Console.WriteLine("4. Search Incidents by Type");
                Console.WriteLine("5. Create Victim");
                Console.WriteLine("6. Generate Report");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Get IncidentID from the user
                        Console.Write("Enter Incident ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int incidentID))
                        {
                            Console.WriteLine("Invalid Incident ID. Please enter a valid integer.");
                            break;
                        }

                        // Get other incident details from the user
                        Console.Write("Enter Incident Type: ");
                        string incidentType = Console.ReadLine();

                        Console.Write("Enter Incident Date (yyyy-mm-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime incidentDate))
                        {
                            Console.WriteLine("Invalid date format. Please enter a valid date.");
                            break;
                        }

                        Console.Write("Enter Location: ");
                        string location = Console.ReadLine();

                        Console.Write("Enter Description: ");
                        string description = Console.ReadLine();

                        Console.Write("Enter Status: ");
                        string status = Console.ReadLine();

                        Console.Write("Enter Victim ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int victimID))
                        {
                            Console.WriteLine("Invalid Victim ID. Please enter a valid integer.");
                            break;
                        }

                        Console.Write("Enter Suspect ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int suspectID))
                        {
                            Console.WriteLine("Invalid Suspect ID. Please enter a valid integer.");
                            break;
                        }

                        // Create Incident object
                        Incident newIncident = new Incident(incidentID,incidentType, incidentDate, location, description, status, victimID, suspectID);

                        // Call service to create incident
                        bool isCreated = crimeService.CreateIncident(newIncident);

                        if (isCreated)
                        {
                            Console.WriteLine("Incident created successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to create incident.");
                        }
                        break;
                    case "2":
                        UpdateIncidentStatus(crimeService);
                        break;
                    case "3":
                        GetIncidentsInDateRange(crimeService);
                        break;
                    case "4":
                        SearchIncidents(crimeService);
                        break;
                    case "5":
                        CreateVictim(crimeService);
                        break;
                    case "6":
                        GenerateReport(crimeService);
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Exiting Crime Analysis and Reporting System.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateIncident(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Incident Type: ");
            string incidentType = Console.ReadLine();

            Console.Write("Enter Incident Date (yyyy-MM-dd HH:mm:ss): ");
            DateTime incidentDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Location: ");
            string location = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Victim ID: ");
            int victimId = int.Parse(Console.ReadLine());

            Console.Write("Enter Suspect ID: ");
            int suspectId = int.Parse(Console.ReadLine());

            Incident newIncident = new Incident
            {
                IncidentType = incidentType,
                IncidentDate = incidentDate,
                Location = location,
                Description = description,
                Status = "Open", // Default status
                VictimID = victimId,
                SuspectID = suspectId
            };

            bool isIncidentCreated = crimeService.CreateIncident(newIncident);
            Console.WriteLine(isIncidentCreated ? "Incident created successfully!" : "Failed to create incident.");
        }

        static void UpdateIncidentStatus(ICrimeAnalysisService crimeService)
        {
            try
            {
                // User input for status and incident ID
                Console.Write("Enter Incident ID to update: ");
                int incidentID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter new status: ");
                string status = Console.ReadLine();

                bool updated = crimeService.UpdateIncidentStatus(status, incidentID);
                if (updated)
                {
                    Console.WriteLine("Incident status updated successfully.");
                }
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        static void GetIncidentsInDateRange(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter start date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter end date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            List<Incident> incidents = crimeService.GetIncidentsInDateRange(startDate, endDate);
            foreach (Incident incident in incidents)
            {
                Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Status: {incident.Status}");
            }
        }

        static void SearchIncidents(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter incident type to search: ");
            string incidentType = Console.ReadLine();

            List<Incident> incidents = crimeService.SearchIncidents(incidentType);
            foreach (Incident incident in incidents)
            {
                Console.WriteLine($"Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Status: {incident.Status}");
            }
        }

        static void CreateVictim(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Victim First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Victim Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Victim Date of Birth (yyyy-MM-dd): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Gender (M/F): ");
            char gender = char.Parse(Console.ReadLine().ToUpper());

            Console.Write("Enter Contact Info: ");
            string contactInfo = Console.ReadLine();

            Victim newVictim = new Victim
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dob,
                Gender = gender,
                ContactInfo = contactInfo
            };

            bool isVictimCreated = crimeService.CreateVictim(newVictim);
            Console.WriteLine(isVictimCreated ? "Victim created successfully!" : "Failed to create victim.");
        }

        static void GenerateReport(ICrimeAnalysisService crimeService)
        {
            Console.Write("Enter Incident ID to generate report for: ");
            int incidentId = int.Parse(Console.ReadLine());

            Report report = crimeService.GenerateIncidentReport(new Incident { IncidentID = incidentId });
            Console.WriteLine($"Generated report with ID: {report.ReportID}, Status: {report.Status}");
        }
    }
}