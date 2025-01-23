using DrivingSchoolAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Dtos
{
    public class TraineeCourseDto
    {
        public int IdTraineeCourse { get; set; }
        public ClientDto? Client { get; set; }
        public ServiceDto? Service{ get; set; }
        public VariantService? VarinatService { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public StatusDto? Status { get; set; }
        public decimal? PESEL { get; set; }
        public string? PKK { get; set; }
        public bool? MedicalCheck { get; set; }
        public bool? ParentalConsent { get; set; }
        public string? Notes { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public CourseDetailsDto? CourseDetails { get; set; }
    }
}
