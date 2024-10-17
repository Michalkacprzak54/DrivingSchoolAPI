using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class City
    {
        [Column("id_miasto")]
        public int IdCity { get; set; }
        [Column("nazwa")]
        public string CityName { get; set; }
    }
}
