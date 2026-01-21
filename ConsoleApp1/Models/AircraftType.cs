using System.ComponentModel.DataAnnotations;

namespace AirportDatabase.Models
{
    public class AircraftType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string TypeName { get; set; }

        public ICollection<Aircraft> Aircrafts { get; set; }
    }
}