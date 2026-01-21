using Microsoft.EntityFrameworkCore;
using AirportDatabase.Models;

namespace AirportDatabase.Data
{
    public class AirportDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<PilotAircraft> PilotsAircraft { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightDestination> FlightDestinations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Airport;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<PilotAircraft>()
                .HasKey(pa => new { pa.AircraftId, pa.PilotId });

            
            modelBuilder.Entity<Passenger>()
                .HasIndex(p => p.FullName).IsUnique();
            modelBuilder.Entity<Passenger>()
                .HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Pilot>()
                .HasIndex(p => p.FirstName).IsUnique();
            modelBuilder.Entity<Pilot>()
                .HasIndex(p => p.LastName).IsUnique();
            modelBuilder.Entity<AircraftType>()
                .HasIndex(at => at.TypeName).IsUnique();
            modelBuilder.Entity<Airport>()
                .HasIndex(a => a.AirportName).IsUnique();
            modelBuilder.Entity<Airport>()
                .HasIndex(a => a.Country).IsUnique();

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AircraftType>().HasData(
                new AircraftType { Id = 1, TypeName = "Private Single Engine" },
                new AircraftType { Id = 2, TypeName = "Twin Turboprops" },
                new AircraftType { Id = 3, TypeName = "Mid-Size Passenger Jets" },
                new AircraftType { Id = 4, TypeName = "Heavy Business Jets" }
            );

            
            modelBuilder.Entity<Pilot>().HasData(
                new Pilot { Id = 1, FirstName = "Genna", LastName = "Jaquet", Age = 32, Rating = 9.5f },
                new Pilot { Id = 2, FirstName = "Jaynell", LastName = "Kidson", Age = 28, Rating = 8.7f },
                new Pilot { Id = 3, FirstName = "Lexie", LastName = "Salasar", Age = 45, Rating = 9.2f },
                new Pilot { Id = 4, FirstName = "Roddie", LastName = "Gribben", Age = 38, Rating = 8.9f },
                new Pilot { Id = 5, FirstName = "Lois", LastName = "Leidle", Age = 29, Rating = 7.8f },
                new Pilot { Id = 6, FirstName = "Delaney", LastName = "Stove", Age = 41, Rating = 9.0f },
                new Pilot { Id = 7, FirstName = "Crosby", LastName = "Godlee", Age = 35, Rating = 8.5f },
                new Pilot { Id = 8, FirstName = "Emma", LastName = "Watson", Age = 31, Rating = 9.1f },
                new Pilot { Id = 9, FirstName = "Oliver", LastName = "Smith", Age = 27, Rating = 8.3f },
                new Pilot { Id = 10, FirstName = "Sophia", LastName = "Johnson", Age = 33, Rating = 9.4f },
                new Pilot { Id = 11, FirstName = "James", LastName = "Brown", Age = 40, Rating = 8.8f },
                new Pilot { Id = 12, FirstName = "Isabella", LastName = "Davis", Age = 26, Rating = 7.9f },
                new Pilot { Id = 13, FirstName = "Michael", LastName = "Miller", Age = 44, Rating = 9.3f },
                new Pilot { Id = 14, FirstName = "Emily", LastName = "Wilson", Age = 30, Rating = 8.6f },
                new Pilot { Id = 15, FirstName = "Daniel", LastName = "Moore", Age = 37, Rating = 9.0f }
            );

            
            modelBuilder.Entity<Aircraft>().HasData(
                new Aircraft { Id = 1, Manufacturer = "Safran", Model = "SaM146", Year = 2018, FlightHours = 559, Condition = "A", TypeId = 1 },
                new Aircraft { Id = 8, Manufacturer = "Safran", Model = "SaM146", Year = 2017, FlightHours = 527, Condition = "B", TypeId = 1 },
                new Aircraft { Id = 13, Manufacturer = "Safran", Model = "PowerJet", Year = 2019, FlightHours = 849, Condition = "A", TypeId = 2 },
                new Aircraft { Id = 25, Manufacturer = "Northrop Grumman", Model = "Bat", Year = 2015, FlightHours = 414, Condition = "C", TypeId = 3 },
                new Aircraft { Id = 37, Manufacturer = "GE Aviation", Model = "CT10", Year = 2020, FlightHours = 4, Condition = "A", TypeId = 4 },
                new Aircraft { Id = 80, Manufacturer = "Lockheed Martin", Model = "F-22 Raptor", Year = 2016, FlightHours = 714, Condition = "B", TypeId = 3 },
                new Aircraft { Id = 100, Manufacturer = "Boeing", Model = "737", Year = 2010, FlightHours = null, Condition = "C", TypeId = 1 },
                new Aircraft { Id = 101, Manufacturer = "Airbus", Model = "A330", Year = 2014, FlightHours = 80, Condition = "B", TypeId = 2 }
            );

            
            modelBuilder.Entity<PilotAircraft>().HasData(
                new PilotAircraft { AircraftId = 1, PilotId = 1 },
                new PilotAircraft { AircraftId = 1, PilotId = 2 },
                new PilotAircraft { AircraftId = 8, PilotId = 3 },
                new PilotAircraft { AircraftId = 13, PilotId = 4 },
                new PilotAircraft { AircraftId = 25, PilotId = 5 },
                new PilotAircraft { AircraftId = 37, PilotId = 6 },
                new PilotAircraft { AircraftId = 80, PilotId = 7 }
            );

            
            modelBuilder.Entity<Airport>().HasData(
                new Airport { Id = 1, AirportName = "Sir Seretse Khama International Airport", Country = "Botswana" },
                new Airport { Id = 2, AirportName = "Kisangani Bangoka International Airport", Country = "Congo" },
                new Airport { Id = 3, AirportName = "Providenciales Airport", Country = "Turks and Caicos" },
                new Airport { Id = 4, AirportName = "Netaji Subhas Chandra Bose International Airport", Country = "India" },
                new Airport { Id = 5, AirportName = "Winnipeg James Armstrong Richardson International Airport", Country = "Canada" }
            );

            
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { Id = 1, FullName = "Zeke Rowston", Email = "ZekeRowston@gmail.com" },
                new Passenger { Id = 2, FullName = "Cullan Dogerty", Email = "CullanDogerty@gmail.com" },
                new Passenger { Id = 3, FullName = "Lanita Crackatt", Email = "LanitaCrackatt@gmail.com" },
                new Passenger { Id = 4, FullName = "Gaye Sillars", Email = "GayeSillars@gmail.com" },
                new Passenger { Id = 5, FullName = "Jacquelynn Plackstone", Email = "JacquelynnPlackstone@gmail.com" },
                new Passenger { Id = 6, FullName = "Danny Simoneau", Email = "DannySimoneau@gmail.com" },
                new Passenger { Id = 7, FullName = "Kaylee Coushe", Email = "KayleeCoushe@gmail.com" },
                new Passenger { Id = 8, FullName = "Parker McGeorge", Email = "ParkerMcGeorge@gmail.com" },
                new Passenger { Id = 9, FullName = "Haven Seaton", Email = "HavenSeaton@gmail.com" }
            );

            
            modelBuilder.Entity<FlightDestination>().HasData(
                new FlightDestination { Id = 1, AirportId = 1, Start = new DateTime(2021, 2, 28, 13, 13, 55), AircraftId = 1, PassengerId = 1, TicketPrice = 3700.65m },
                new FlightDestination { Id = 2, AirportId = 2, Start = new DateTime(2020, 7, 2, 15, 27, 47), AircraftId = 8, PassengerId = 2, TicketPrice = 5048.89m },
                new FlightDestination { Id = 3, AirportId = 3, Start = new DateTime(2020, 2, 6, 22, 32, 14), AircraftId = 13, PassengerId = 3, TicketPrice = 4100.49m },
                new FlightDestination { Id = 4, AirportId = 4, Start = new DateTime(2021, 2, 20, 21, 4, 53), AircraftId = 25, PassengerId = 4, TicketPrice = 4002.21m },
                new FlightDestination { Id = 5, AirportId = 5, Start = new DateTime(2020, 11, 28, 17, 58, 40), AircraftId = 37, PassengerId = 5, TicketPrice = 3390.81m },
                new FlightDestination { Id = 6, AirportId = 1, Start = new DateTime(2020, 9, 10, 1, 55, 19), AircraftId = 80, PassengerId = 6, TicketPrice = 3690.22m },
                new FlightDestination { Id = 7, AirportId = 1, Start = new DateTime(2020, 6, 4, 8, 30, 0), AircraftId = 1, PassengerId = 7, TicketPrice = 890.50m },
                new FlightDestination { Id = 8, AirportId = 2, Start = new DateTime(2020, 8, 12, 14, 20, 0), AircraftId = 1, PassengerId = 8, TicketPrice = 1200.00m }
            );
        }
    }
}