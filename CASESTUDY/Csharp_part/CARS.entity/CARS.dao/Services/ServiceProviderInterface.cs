using CARS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.dao.Services
{
    public interface ICrimeAnalysisService
    {

        bool CreateIncident(Incident incident);


        bool UpdateIncidentStatus(string status, int incidentID);


        List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);


        List<Incident> SearchIncidents(string incidentType);


        Report GenerateIncidentReport(Incident incident);


        bool CreateVictim(Victim victim);


        bool CreateSuspect(Suspect suspect);


        bool CreateOfficer(Officer officer);
        bool CreateLawEnforcementAgency(LawEnforcementAgency agency);
        bool CreateEvidence(Evidence evidence);
        bool CreateReport(Report report);
    }
}
