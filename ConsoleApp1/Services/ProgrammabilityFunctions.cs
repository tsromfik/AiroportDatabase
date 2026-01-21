using Microsoft.EntityFrameworkCore;
using AirportDatabase.Data;

namespace AirportDatabase.Services
{
    public class ProgrammabilityFunctions
    {
        private readonly AirportDbContext _context;

        public ProgrammabilityFunctions(AirportDbContext context)
        {
            _context = context;
        }

        public int UDF_FlightDestinationsByEmail(string email)
        {
            var passenger = _context.Passengers
                .Include(p => p.FlightDestinations)
                .FirstOrDefault(p => p.Email == email);

            return passenger?.FlightDestinations.Count ?? 0;
        }

        public List<object> USP_SearchByAirportName(string airportName)
        {
            var result = _context.FlightDestinations
                .Include(fd => fd.Airport)
                .Include(fd => fd.Passenger)
                .Include(fd => fd.Aircraft)
                    .ThenInclude(a => a.Type)
                .Where(fd => fd.Airport.AirportName == airportName)
                .Select(fd => new
                {
                    AirportName = fd.Airport.AirportName,
                    FullName = fd.Passenger.FullName,
                    LevelOfTicketPrice = fd.TicketPrice <= 400 ? "Low" :
                                        fd.TicketPrice <= 1500 ? "Medium" : "High",
                    Manufacturer = fd.Aircraft.Manufacturer,
                    Condition = fd.Aircraft.Condition,
                    TypeName = fd.Aircraft.Type.TypeName
                })
                .OrderBy(x => x.Manufacturer)
                .ThenBy(x => x.FullName)
                .ToList<object>();

            return result;
        }
    }
}