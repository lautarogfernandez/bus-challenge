namespace BusApi.Models
{
    public class BusDto
    {
        public string RegistrationPlate { get; set; }
        public Guid DriverId { get; set; }
        public IEnumerable<Guid> KidIds { get; set; }
    }
}