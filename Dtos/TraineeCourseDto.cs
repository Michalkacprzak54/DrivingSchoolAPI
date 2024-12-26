using DrivingSchoolAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Dtos
{
    public class TraineeCourseDto
    {
        public int IdTraineeCourse { get; set; }
        public ClientDto? Client { get; set; }
        public ClientServiceDto? ClientService { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public StatusDto? Status { get; set; }
        public decimal? PESEL { get; set; }
        public string? PKK { get; set; }
        public bool? MedicalCheck { get; set; }
        public string? Notes { get; set; }
    }
}
