namespace DrivingSchoolAPI.Dtos
{
    public class ClientServiceDto
    {
        public int? IdClientService { get; set; }
        public ClientDto? Client { get; set; }
        public ServiceDto? Service { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string? Status { get; set; }
    }

}
