namespace DrivingSchoolAPI.Dtos
{
    public class InstructorDetailsDto
    {
        public string? InstructorFirstName { get; set; }
        public string? InstructorLastName { get; set; }
        public string? InstructorPhoneNumber { get; set; }
        public string? InstructorEmail { get; set; }
        public bool? InstructorTeachesPractice { get; set; }
        public bool? InstructorTeachesTheory { get; set; }
        public DateOnly? InstructorDateOfBirth { get; set; }
        public string? InstructorPassword { get; set; }
        public decimal? InstructorPesel { get; set; }
        public string? InstructorCity { get; set; }
        public string? InstructorZipCode { get; set; }
        public string? InstructorStreet { get; set; }
        public string? InstructorHouseNumber { get; set; }
        public int? InstructorFlatNumber { get; set; }
    }

}
