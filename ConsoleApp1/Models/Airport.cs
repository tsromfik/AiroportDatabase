using System.ComponentModel.DataAnnotations;

namespace AirportDatabase.Models
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string AirportName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        public ICollection<FlightDestination> FlightDestinations { get; set; }
    }
}