namespace BusApi.Models
{
    public class DriverListResponse
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string? BusRegistrationPlate { get; set; }
    }
}