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
        public string? Notes { get; set; }
        public bool IsUsed { get; set; }
        public int HowManyUsed { get; set; }
        public bool? BasicPractice { get; set; }
        public bool? ExtendedPractice { get; set; }
        public bool? OnlineTheory { get; set; }
        public bool? StationaryTheory { get; set; }
        public bool? TheoryCompleted { get; set; }
        public bool? Manual { get; set; }
        public bool? Automatic { get; set; }
    }

}
