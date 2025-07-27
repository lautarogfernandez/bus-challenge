using System.ComponentModel.DataAnnotations;

namespace BusApi.Domain
{
    public class Kid
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string DocumentNumber { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        public Guid? BusId { get; set; }
        public Bus? Bus { get; set; }
    }
}