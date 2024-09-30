CREATE DATABASE C_A_R_S;
USE C_A_R_S;

CREATE TABLE Incidents (
    IncidentID INT PRIMARY KEY,
    IncidentType VARCHAR(50),
    IncidentDate DATE,
    Location GEOGRAPHY,  
    Description TEXT,
    Status VARCHAR(20),
    VictimID INT,
    SuspectID INT,
    FOREIGN KEY (VictimID) REFERENCES Victims(VictimID),
    FOREIGN KEY (SuspectID) REFERENCES Suspects(SuspectID)
);
CREATE TABLE Victims (
    VictimID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DateOfBirth DATE,
    Gender CHAR(1),
    ContactInformation VARCHAR(255)
);

CREATE TABLE Suspects (
    SuspectID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DateOfBirth DATE,
    Gender CHAR(1),
    ContactInformation VARCHAR(255)
);

CREATE TABLE LawEnforcementAgencies (
    AgencyID INT PRIMARY KEY,
    AgencyName VARCHAR(100),
    Jurisdiction VARCHAR(100),
    ContactInformation VARCHAR(255),
	Officers VARCHAR(100)
);

CREATE TABLE Officers (
    OfficerID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    BadgeNumber VARCHAR(20),
    Rank VARCHAR(50),
    ContactInformation VARCHAR(255),
    AgencyID INT,
    FOREIGN KEY (AgencyID) REFERENCES LawEnforcementAgencies(AgencyID)
);

CREATE TABLE Evidence (
    EvidenceID INT PRIMARY KEY,
    Description TEXT,
    LocationFound VARCHAR(255),
    IncidentID INT,
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID)
);

CREATE TABLE Reports (
    ReportID INT PRIMARY KEY,
    IncidentID INT,
    ReportingOfficer INT,
    ReportDate DATE,
    ReportDetails TEXT,
    Status VARCHAR(20),
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID),
    FOREIGN KEY (ReportingOfficer) REFERENCES Officers(OfficerID)
);

INSERT INTO Incidents (IncidentID, IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID) VALUES
(1, 'Robbery', '2024-07-01', geography::STGeomFromText('POINT(28.6139 77.2090)', 4326), 'Robbery at Connaught Place, Delhi', 'Closed', 1, 1),
(2, 'Homicide', '2024-06-15', geography::STGeomFromText('POINT(19.0760 72.8777)', 4326), 'Homicide in a Mumbai apartment', 'Under Investigation', 2, 2),
(3, 'Theft', '2024-05-20', geography::STGeomFromText('POINT(13.0827 80.2707)', 4326), 'Theft at a shop in Chennai', 'Open', 3, 3),
(4, 'Assault', '2024-07-10', geography::STGeomFromText('POINT(17.3850 78.4867)', 4326), 'Assault in Hyderabad', 'Closed', 4, 4),
(5, 'Robbery', '2024-08-05', geography::STGeomFromText('POINT(22.5726 88.3639)', 4326), 'Bank robbery in Kolkata', 'Under Investigation', 5, 5),
(6, 'Homicide', '2024-09-01', geography::STGeomFromText('POINT(23.0225 72.5714)', 4326), 'Murder in Ahmedabad', 'Open', 6, 6),
(7, 'Burglary', '2024-06-30', geography::STGeomFromText('POINT(12.9716 77.5946)', 4326), 'Burglary at a house in Bengaluru', 'Closed', 7, 7),
(8, 'Kidnapping', '2024-07-20', geography::STGeomFromText('POINT(18.5204 73.8567)', 4326), 'Kidnapping incident in Pune', 'Open', 8, 8),
(9, 'Arson', '2024-08-15', geography::STGeomFromText('POINT(26.9124 75.7873)', 4326), 'Arson attack on a shop in Jaipur', 'Under Investigation', 9, 9),
(10, 'Vandalism', '2024-09-12', geography::STGeomFromText('POINT(8.5241 76.9366)', 4326), 'Vandalism of public property in Thiruvananthapuram', 'Closed', 10, 10);

INSERT INTO Victims (VictimID, FirstName, LastName, DateOfBirth, Gender, ContactInformation) VALUES
(1, 'Rajesh', 'Kumar', '1985-05-12', 'M', '123 North Street, Delhi, 9876543210'),
(2, 'Pooja', 'Sharma', '1990-03-15', 'F', '456 East Road, Delhi, 9876543211'),
(3, 'Arvind', 'Verma', '1978-11-22', 'M', '789 South Lane, Mumbai, 9876543212'),
(4, 'Lakshmi', 'Nair', '1992-04-19', 'F', '321 West Avenue, Chennai, 9876543213'),
(5, 'Neha', 'Gupta', '1989-09-30', 'F', '654 Park Street, Kolkata, 9876543214'),
(6, 'Suresh', 'Yadav', '1975-12-07', 'M', '987 Central Road, Lucknow, 9876543215'),
(7, 'Dinesh', 'Reddy', '1982-02-14', 'M', '147 Riverbank, Hyderabad, 9876543216'),
(8, 'Priya', 'Mehta', '1991-08-25', 'F', '321 Green Valley, Pune, 9876543217'),
(9, 'Venkatesh', 'Iyer', '1980-07-04', 'M', '789 Lakeview, Bengaluru, 9876543218'),
(10, 'Sunita', 'Chauhan', '1988-01-16', 'F', '135 Oak Street, Jaipur, 9876543219');

INSERT INTO Suspects (SuspectID, FirstName, LastName, DateOfBirth, Gender, ContactInformation) VALUES
(1, 'Raghavendra', 'Rao', '1977-06-30', 'M', '9A Marina Street, Chennai, 8876543210'),
(2, 'Ankit', 'Verma', '1981-03-10', 'M', '12B Pearl Road, Jaipur, 8876543211'),
(3, 'Kavita', 'Sharma', '1993-07-18', 'F', '14C Lotus Avenue, Delhi, 8876543212'),
(4, 'Aravind', 'Swaminathan', '1984-10-02', 'M', '18D Jasmine Lane, Hyderabad, 8876543213'),
(5, 'Dinesh', 'Nair', '1986-05-28', 'M', '20E Palm Street, Kochi, 8876543214'),
(6, 'Priya', 'Reddy', '1990-11-15', 'F', '23F Cedar Road, Bengaluru, 8876543215'),
(7, 'Suresh', 'Patel', '1988-01-25', 'M', '25G Oak Lane, Ahmedabad, 8876543216'),
(8, 'Sunita', 'Yadav', '1992-02-07', 'F', '28H Maple Avenue, Bhopal, 8876543217'),
(9, 'Rajesh', 'Singh', '1979-09-09', 'M', '30I Birch Road, Delhi, 8876543218'),
(10, 'Lakshmi', 'Menon', '1985-03-12', 'F', '35J Banyan Lane, Thiruvananthapuram, 8876543219');

INSERT INTO LawEnforcementAgencies (AgencyID, AgencyName, Jurisdiction, ContactInformation, Officers) VALUES
(1, 'Delhi Police', 'Delhi', 'HQ: North Block, Delhi, 011-1234567', 'Rakesh Sharma, Anil Kumar'),
(2, 'Mumbai Police', 'Mumbai', 'HQ: Crawford Market, Mumbai, 022-1234567', 'Ajay Mehta, Sunita Yadav'),
(3, 'Chennai Police', 'Chennai', 'HQ: Ezhilagam Building, Chennai, 044-1234567', 'Ravi Iyer, Priya Reddy'),
(4, 'Hyderabad Police', 'Hyderabad', 'HQ: Basheerbagh, Hyderabad, 040-1234567', 'Srinivas Reddy, Meena Gupta'),
(5, 'Kochi Police', 'Kochi', 'HQ: Marine Drive, Kochi, 0484-1234567', 'Dinesh Nair, Lakshmi Menon'),
(6, 'Ahmedabad Police', 'Ahmedabad', 'HQ: Shahibaug, Ahmedabad, 079-1234567', 'Kiran Patel, Anjali Joshi'),
(7, 'Bengaluru Police', 'Bengaluru', 'HQ: Infantry Road, Bengaluru, 080-1234567', 'Rajesh Naik, Kavita Rani'),
(8, 'Pune Police', 'Pune', 'HQ: Shivaji Nagar, Pune, 020-1234567', 'Manoj Joshi, Pooja Sharma'),
(9, 'Jaipur Police', 'Jaipur', 'HQ: Lal Kothi, Jaipur, 0141-1234567', 'Suresh Singh, Neha Agarwal'),
(10, 'Kerala Police', 'Thiruvananthapuram', 'HQ: Police Headquarters, Thiruvananthapuram, 0471-1234567', 'Arjun Nair, Maya Ramesh');

INSERT INTO Officers (OfficerID, FirstName, LastName, BadgeNumber, Rank, ContactInformation, AgencyID) VALUES
(1, 'Rakesh', 'Sharma', 'B1234', 'Inspector', '35 North Road, Delhi, 8876543220', 1),
(2, 'Anil', 'Kumar', 'B1235', 'Sergeant', '45 South Street, Mumbai, 8876543221', 2),
(3, 'Ravi', 'Iyer', 'B1236', 'Inspector', '55 East Avenue, Chennai, 8876543222', 3),
(4, 'Srinivas', 'Reddy', 'B1237', 'Constable', '65 West Lane, Hyderabad, 8876543223', 4),
(5, 'Arun', 'Nair', 'B1238', 'Sergeant', '75 Park Street, Kochi, 8876543224', 5),
(6, 'Kiran', 'Patel', 'B1239', 'Inspector', '85 River Road, Ahmedabad, 8876543225', 6),
(7, 'Meena', 'Verma', 'B1240', 'Constable', '95 Lakeview, Bengaluru, 8876543226', 7),
(8, 'Priyanka', 'Gupta', 'B1241', 'Inspector', '105 Mountain Street, Pune, 8876543227', 8),
(9, 'Manoj', 'Chauhan', 'B1242', 'Sergeant', '115 Valley Lane, Jaipur, 8876543228', 9),
(10, 'Sandeep', 'Swaminathan', 'B1243', 'Constable', '125 Oakwood, Thiruvananthapuram, 8876543229', 10);

INSERT INTO Evidence (EvidenceID, Description, LocationFound, IncidentID) VALUES
(1, 'Fingerprint evidence collected from the crime scene.', 'Connaught Place, Delhi', 1),
(2, 'Surveillance footage showing the suspect.', 'Mumbai Apartment', 2),
(3, 'Stolen goods recovered from the shop.', 'Chennai Store', 3),
(4, 'Witness statements collected from bystanders.', 'Hyderabad Street', 4),
(5, 'CCTV footage from the bank.', 'Kolkata Bank', 5),
(6, 'Blood samples collected at the scene.', 'Ahmedabad Apartment', 6),
(7, 'Security camera footage from nearby houses.', 'Bengaluru Residential Area', 7),
(8, 'Cell phone found at the scene.', 'Pune Park', 8),
(9, 'Arson debris samples collected.', 'Jaipur Shop', 9),
(10, 'Graffiti and vandalized property pictures.', 'Thiruvananthapuram', 10);

INSERT INTO Reports (ReportID, IncidentID, ReportingOfficer, ReportDate, ReportDetails, Status) VALUES
(1, 1, 1, '2024-07-02', 'Initial report filed regarding robbery at Connaught Place.', 'Draft'),
(2, 2, 2, '2024-06-16', 'Homicide case report filed with all initial findings.', 'Finalized'),
(3, 3, 3, '2024-05-21', 'Theft report filed after investigation at the shop.', 'Draft'),
(4, 4, 4, '2024-07-11', 'Assault case report with witness statements.', 'Finalized'),
(5, 5, 5, '2024-08-06', 'Detailed report on bank robbery incident.', 'Draft'),
(6, 6, 6, '2024-09-02', 'Homicide investigation report filed.', 'Finalized'),
(7, 7, 7, '2024-06-30', 'Burglary report filed with collected evidence.', 'Draft'),
(8, 8, 8, '2024-07-21', 'Kidnapping report filed with details of the incident.', 'Finalized'),
(9, 9, 9, '2024-08-16', 'Arson report with preliminary findings.', 'Draft'),
(10, 10, 10, '2024-09-13', 'Vandalism report filed after damage assessment.', 'Finalized');









