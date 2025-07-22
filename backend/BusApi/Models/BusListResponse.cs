namespace BusApi.Models
{
    public class BusListResponse
    {
        public int Id { get; set; }
        public string RegistrationPlate { get; set; }
        public string Driver { get; set; }
        public int Children { get; set; }
    }
}