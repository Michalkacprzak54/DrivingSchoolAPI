using DrivingSchoolMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class Photo
    {
        [Column("id_zdjecie")]
        public int IdPhoto { get; set; }

        [Column("id_usluga")]
        public int IdService { get; set; }

        [Column("sciezka_zdjecie")]
        public string PhotoPath { get; set; }

        [Column("opis_alternatywny")]
        public string AlternativeDescription { get; set; }

        [JsonIgnore]
        public Service? Servcie { get; set; }
    }
}
