using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Bus
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string RegistrationPlate { get; set; }
        [Required]
        public Driver Driver { get; set; }
        public Guid DriverId { get; set; }
        public List<Kid>? Kids { get; set; }
    }
}