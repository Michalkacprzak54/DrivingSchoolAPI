namespace DrivingSchoolAPI.Dtos
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public int? FlatNumber { get; set; }
    }
}
