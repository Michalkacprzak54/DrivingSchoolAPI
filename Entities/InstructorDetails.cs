using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class InstructorDetails
    {
        [Column("id_szczegoly_instruktor")]
        public int IdInstructorDetails { get; set; }
        [Column("id_instruktor")]
        public int IdInstructor { get; set; }
        [Column("haslo")]
        public string InstructorPassword { get; set; }
        [Column("pesel")]
        public decimal InstructorPesel { get; set; }
        [Column("id_miasto")]
        public int InstructorCityId { get; set; }
        [Column("id_kod_pocztowy")]
        public int InstructorZipCodeId { get; set; }
        [Column("ulica")]
        public string InstructorStreet { get; set; }
        [Column("nr_domu")]
        public string InstructorHouseNumber { get; set; }
        [Column("nr_lokalu")]
        public int? InstructorFlatNumber { get; set; }

        [JsonIgnore]
        public Instructor Instructor { get; set; }
        public City City { get; set; }
        public ZipCode ZipCode { get; set; }


    }
}
