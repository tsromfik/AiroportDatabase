using System.ComponentModel.DataAnnotations;

namespace AirportDatabase.Models
{
    public class Pilot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [Range(21, 62)]
        public byte Age { get; set; }

        [Range(0.0, 10.0)]
        public float? Rating { get; set; }

        public ICollection<PilotAircraft> PilotAircrafts { get; set; }
    }
}