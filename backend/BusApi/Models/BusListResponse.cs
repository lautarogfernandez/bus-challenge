namespace BusApi.Models
{
    public class BusListResponse
    {
        public Guid Id { get; set; }
        public string RegistrationPlate { get; set; }
        public string DriverDocumentNumber { get; set; }
        public int Kids { get; set; }
    }
}