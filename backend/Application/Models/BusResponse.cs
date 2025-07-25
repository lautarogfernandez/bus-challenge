namespace BusApi.Models
{
    public class BusResponse
    {
        public Guid Id { get; set; }
        public string RegistrationPlate { get; set; }
        public Guid DriverId { get; set; }
        public List<Guid> KidIds { get; set; }
    }
}