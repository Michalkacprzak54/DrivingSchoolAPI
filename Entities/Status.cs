using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class Status
    {
        [Column("id_status")]
        public int IdStatus { get; set; }
        [Column("status")]
        public string StatusName { get; set; }

        [JsonIgnore]
        public ServiceSchedule ServiceSchedule { get; set; }   
    }
}
