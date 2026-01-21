using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportDatabase.Models
{
    public class PilotAircraft
    {
        [Required]
        public int AircraftId { get; set; }

        [Required]
        public int PilotId { get; set; }

        [ForeignKey(nameof(AircraftId))]
        public Aircraft Aircraft { get; set; }

        [ForeignKey(nameof(PilotId))]
        public Pilot Pilot { get; set; }
    }
}