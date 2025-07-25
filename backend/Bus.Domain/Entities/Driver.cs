using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string DocumentNumber { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        public Bus? Bus { get; set; }
    }
}