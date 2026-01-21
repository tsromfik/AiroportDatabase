using System.ComponentModel.DataAnnotations;

namespace AirportDatabase.Models
{
    public class Passenger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public ICollection<FlightDestination> FlightDestinations { get; set; }
    }
}