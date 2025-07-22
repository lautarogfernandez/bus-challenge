namespace BusApi.Models
{
    public class BusResponse
    {
        public int Id { get; set; }
        public string RegistrationPlate { get; set; }
        public int DriverId { get; set; }
        public IEnumerable<int> ChildrenIds { get; set; }
    }
}