using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class StatusDto
    {
        public int IdStatus { get; set; }
        public string? StatusName { get; set; }
    }
}
