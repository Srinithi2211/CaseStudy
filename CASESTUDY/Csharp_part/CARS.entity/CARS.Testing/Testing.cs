using NUnit.Framework;
using CARS.dao;
using CARS.dao.Services;
using System;
using System.Data.SqlClient;
using CARS.dao.Repository;
using CARS.entity;
using CARS.exception;

namespace CrimeAnalysisTests
{
    [TestFixture]
    public class CrimeAnalysisServiceImplTests
    {
        private CrimeAnalysisServiceImpl _service;

        [SetUp]
        public void Setup()
        {
            // Initialize CrimeAnalysisServiceImpl before each test
            _service = new CrimeAnalysisServiceImpl();
        }

        [Test]
        public void CreateIncident_ShouldCreateIncidentSuccessfully()
        {
            // Arrange
            var incident = new Incident
            {
                IncidentType = "Robbery",
                IncidentDate = DateTime.Now,
                Location = "Downtown",
                Description = "Bank Robbery",
                Status = "Open",
                VictimID = 1,
                SuspectID = 2
            };

            // Act
            var result = _service.CreateIncident(incident);

            // Assert
            Assert.IsTrue(result, "Incident should be created successfully.");
        }

        [Test]
        public void UpdateIncidentStatus_ShouldUpdateSuccessfully()
        {
            // Arrange
            int incidentID = 1;  // Assume incident with ID 1 exists.
            string newStatus = "Closed";

            // Act
            var result = _service.UpdateIncidentStatus(newStatus, incidentID);

            // Assert
            Assert.IsTrue(result, "Incident status should be updated successfully.");
        }

        [Test]
        public void UpdateIncidentStatus_ShouldThrowIncidentNumberNotFoundException()
        {
            // Arrange
            int invalidIncidentID = 999;  // Non-existing incident ID.
            string newStatus = "Closed";

            // Act & Assert
            var ex = Assert.Throws<IncidentNumberNotFoundException>(() =>
            {
                _service.UpdateIncidentStatus(newStatus, invalidIncidentID);
            });

            Assert.That(ex.Message, Is.EqualTo($"Incident with ID {invalidIncidentID} not found."));
        }

        [Test]
        public void CreateIncident_WithInvalidData_ShouldThrowException()
        {
            // Arrange
            var incident = new Incident
            {
                IncidentType = null,  // Invalid incident type (null)
                IncidentDate = DateTime.Now,
                Location = "Downtown",
                Description = "Test Description",
                Status = "Open",
                VictimID = 1,
                SuspectID = 2
            };

            // Act & Assert
            Assert.Throws<SqlException>(() => _service.CreateIncident(incident), "Creating incident with invalid data should throw an exception.");
        }
    }
}
