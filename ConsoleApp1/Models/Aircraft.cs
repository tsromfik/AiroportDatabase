using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportDatabase.Models
{
    public class Aircraft
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Manufacturer { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        public int? FlightHours { get; set; }

        [Required]
        [MaxLength(1)]
        public string Condition { get; set; }

        [Required]
        public int TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public AircraftType Type { get; set; }

        public ICollection<PilotAircraft> PilotAircrafts { get; set; }
        public ICollection<FlightDestination> FlightDestinations { get; set; }
    }
}