namespace DrivingSchoolAPI.Dtos
{
    public class ServiceDto
    {
        public int IdService { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public string? ServicePlace { get; set; }
        public string? ServiceCategory { get; set; }
        // Dodaj inne pola, które chcesz zwrócić
    }
}
