using AirportDatabase.Data;
using AirportDatabase.Services;

namespace AirportDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AirportDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Console.WriteLine("Database 'Airport' created successfully!\n");

                var dml = new DataManipulation(context);
                dml.InsertPassengersFromPilots();
                dml.UpdateAircraftConditions();

                var queries = new Queries(context);
                queries.GetAircraftByFlightHours();
                queries.GetPilotsAndAircraft();
                queries.GetTop20FlightDestinations();
                queries.GetFlightCountPerAircraft();
                queries.GetRegularPassengers();
                queries.GetFullFlightInformation();

                var functions = new ProgrammabilityFunctions(context);

                Console.WriteLine("\n11. UDF - Flight Destinations by Email:");
                Console.WriteLine($"ZekeRowston@gmail.com: {functions.UDF_FlightDestinationsByEmail("ZekeRowston@gmail.com")}");
                Console.WriteLine($"LanitaCrackatt@gmail.com: {functions.UDF_FlightDestinationsByEmail("LanitaCrackatt@gmail.com")}");
                Console.WriteLine($"Unknown@gmail.com: {functions.UDF_FlightDestinationsByEmail("Unknown@gmail.com")}");

                Console.WriteLine("\n12. USP - Search by Airport Name:");
                var airportResults = functions.USP_SearchByAirportName("Sir Seretse Khama International Airport");
                foreach (dynamic r in airportResults)
                    Console.WriteLine($"{r.AirportName}\t{r.FullName}\t{r.LevelOfTicketPrice}\t{r.Manufacturer}\t{r.Condition}\t{r.TypeName}");

                Console.WriteLine("\nAll tasks completed successfully!");
            }
        }
    }
}