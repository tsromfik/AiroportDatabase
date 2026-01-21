using Microsoft.EntityFrameworkCore;
using AirportDatabase.Data;

namespace AirportDatabase.Services
{
    public class Queries
    {
        private readonly AirportDbContext _context;

        public Queries(AirportDbContext context)
        {
            _context = context;
        }

        public void GetAircraftByFlightHours()
        {
            var result = _context.Aircrafts
                .OrderByDescending(a => a.FlightHours)
                .Select(a => new
                {
                    a.Manufacturer,
                    a.Model,
                    a.FlightHours,
                    a.Condition
                })
                .ToList();

            Console.WriteLine("\n5. Aircraft ordered by FlightHours:");
            foreach (var a in result)
                Console.WriteLine($"{a.Manufacturer}\t{a.Model}\t{a.FlightHours}\t{a.Condition}");
        }

        public void GetPilotsAndAircraft()
        {
            var result = _context.PilotsAircraft
                .Include(pa => pa.Pilot)
                .Include(pa => pa.Aircraft)
                .Where(pa => pa.Aircraft.FlightHours != null && pa.Aircraft.FlightHours > 304)
                .OrderByDescending(pa => pa.Aircraft.FlightHours)
                .ThenBy(pa => pa.Pilot.FirstName)
                .Select(pa => new
                {
                    pa.Pilot.FirstName,
                    pa.Pilot.LastName,
                    pa.Aircraft.Manufacturer,
                    pa.Aircraft.Model,
                    pa.Aircraft.FlightHours
                })
                .ToList();

            Console.WriteLine("\n6. Pilots and Aircraft (FlightHours > 304):");
            foreach (var p in result)
                Console.WriteLine($"{p.FirstName}\t{p.LastName}\t{p.Manufacturer}\t{p.Model}\t{p.FlightHours}");
        }

        public void GetTop20FlightDestinations()
        {
            var result = _context.FlightDestinations
                .Include(fd => fd.Passenger)
                .Include(fd => fd.Airport)
                .Where(fd => fd.Start.Day % 2 == 0)
                .OrderByDescending(fd => fd.TicketPrice)
                .ThenBy(fd => fd.Airport.AirportName)
                .Take(20)
                .Select(fd => new
                {
                    DestinationId = fd.Id,
                    fd.Start,
                    FullName = fd.Passenger.FullName,
                    AirportName = fd.Airport.AirportName,
                    fd.TicketPrice
                })
                .ToList();

            Console.WriteLine("\n7. Top 20 Flight Destinations (even day):");
            foreach (var f in result)
                Console.WriteLine($"{f.DestinationId}\t{f.Start}\t{f.FullName}\t{f.AirportName}\t{f.TicketPrice}");
        }

        public void GetFlightCountPerAircraft()
        {
            var result = _context.Aircrafts
                .Where(a => a.FlightDestinations.Count >= 2)
                .Select(a => new
                {
                    AircraftId = a.Id,
                    a.Manufacturer,
                    a.FlightHours,
                    FlightDestinationsCount = a.FlightDestinations.Count,
                    AvgPrice = a.FlightDestinations.Average(fd => fd.TicketPrice)
                })
                .OrderByDescending(a => a.FlightDestinationsCount)
                .ThenBy(a => a.AircraftId)
                .ToList();

            Console.WriteLine("\n8. Flight Count per Aircraft (min 2):");
            foreach (var a in result)
                Console.WriteLine($"{a.AircraftId}\t{a.Manufacturer}\t{a.FlightHours}\t{a.FlightDestinationsCount}\t{a.AvgPrice:F2}");
        }

        public void GetRegularPassengers()
        {
            var passengersData = _context.Passengers
                .Include(p => p.FlightDestinations)
                .ToList();

            var result = passengersData
                .Where(p => p.FullName.Length > 1 &&
                           p.FullName[1] == 'a' &&
                           p.FlightDestinations.Select(fd => fd.AircraftId).Distinct().Count() > 1)
                .Select(p => new
                {
                    p.FullName,
                    CountOfAircraft = p.FlightDestinations
                        .Select(fd => fd.AircraftId)
                        .Distinct()
                        .Count(),
                    TotalPayed = p.FlightDestinations.Sum(fd => fd.TicketPrice)
                })
                .OrderBy(p => p.FullName)
                .ToList();

            Console.WriteLine("\n9. Regular Passengers:");
            foreach (var p in result)
                Console.WriteLine($"{p.FullName}\t{p.CountOfAircraft}\t{p.TotalPayed:F2}");
        }

        public void GetFullFlightInformation()
        {
            var result = _context.FlightDestinations
                .Include(fd => fd.Airport)
                .Include(fd => fd.Passenger)
                .Include(fd => fd.Aircraft)
                .Where(fd => fd.Start.Hour >= 6 &&
                           fd.Start.Hour <= 20 &&
                           fd.TicketPrice > 2500)
                .OrderBy(fd => fd.Aircraft.Model)
                .Select(fd => new
                {
                    AirportName = fd.Airport.AirportName,
                    DayTime = fd.Start,
                    fd.TicketPrice,
                    FullName = fd.Passenger.FullName,
                    Manufacturer = fd.Aircraft.Manufacturer,
                    Model = fd.Aircraft.Model
                })
                .ToList();

            Console.WriteLine("\n10. Full Flight Information:");
            foreach (var f in result)
                Console.WriteLine($"{f.AirportName}\t{f.DayTime}\t{f.TicketPrice}\t{f.FullName}\t{f.Manufacturer}\t{f.Model}");
        }
    }
}