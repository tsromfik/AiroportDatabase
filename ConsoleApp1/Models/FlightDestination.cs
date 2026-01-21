using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportDatabase.Models
{
    public class FlightDestination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AirportId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public int PassengerId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } = 15.00m;

        [ForeignKey(nameof(AirportId))]
        public Airport Airport { get; set; }

        [ForeignKey(nameof(AircraftId))]
        public Aircraft Aircraft { get; set; }

        [ForeignKey(nameof(PassengerId))]
        public Passenger Passenger { get; set; }
    }
}