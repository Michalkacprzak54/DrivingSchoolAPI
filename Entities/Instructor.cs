using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class Instructor
    {
        [Column("id_instruktor")]
        public int IdInstructor { get; set; }
        [Column("imie")]
        public string InstructorFirstName { get; set; }
        [Column("nazwisko")]
        public string InstructorLastName { get; set; }
        [Column("numer_telefonu")]
        public string InstructorPhhoneNumber { get; set; }
        [Column("email")]
        public string InstructorEmail { get; set; }
        [Column("czy_prowadzi_praktyke")]
        public bool InstructorPratice { get; set; }
        [Column("czy_prowadzi_teorie")]
        public bool InstructorTheory { get; set; }

        [JsonIgnore]
        public InstructorDetails? InstructorDetails { get; set; }

        //[JsonIgnore]
        public ICollection<InscrutorEntitlement> InstructorEntitlements { get; set; }


    }
}
