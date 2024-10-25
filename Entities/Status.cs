using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class Status
    {
        [Column("id_status")]
        public int IdStatus { get; set; }
        [Column("status")]
        public string StatusName { get; set; }
    }
}
