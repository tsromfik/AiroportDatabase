using AirportDatabase.Data;
using AirportDatabase.Models;

namespace AirportDatabase.Services
{
    public class DataManipulation
    {
        private readonly AirportDbContext _context;

        public DataManipulation(AirportDbContext context)
        {
            _context = context;
        }

        public void InsertPassengersFromPilots()
        {
            var pilots = _context.Pilots
                .Where(p => p.Id >= 5 && p.Id <= 15)
                .ToList();

            foreach (var pilot in pilots)
            {
                var fullName = $"{pilot.FirstName} {pilot.LastName}";
                var email = $"{pilot.FirstName}{pilot.LastName}@gmail.com";

                if (!_context.Passengers.Any(p => p.Email == email))
                {
                    _context.Passengers.Add(new Passenger
                    {
                        FullName = fullName,
                        Email = email
                    });
                }
            }

            _context.SaveChanges();
            Console.WriteLine("Passengers inserted from Pilots (ID 5-15)");
        }

        public void UpdateAircraftConditions()
        {
            var aircrafts = _context.Aircrafts
                .Where(a => (a.Condition == "C" || a.Condition == "B") &&
                           (a.FlightHours == null || a.FlightHours <= 100) &&
                           a.Year >= 2013)
                .ToList();

            foreach (var aircraft in aircrafts)
            {
                aircraft.Condition = "A";
            }

            _context.SaveChanges();
            Console.WriteLine($"Updated {aircrafts.Count} aircraft conditions to 'A'");
        }

        public void DeletePassengersWithShortNames()
        {
            var passengersToDelete = _context.Passengers
                .Where(p => p.FullName.Length <= 10)
                .ToList();

            _context.Passengers.RemoveRange(passengersToDelete);
            _context.SaveChanges();
            Console.WriteLine($"Deleted {passengersToDelete.Count} passengers");
        }
    }
}