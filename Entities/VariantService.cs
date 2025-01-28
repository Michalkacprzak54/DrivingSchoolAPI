using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class VariantService
    {
        [Column("id_wariant_usluga")]
        public int? IdVariantService { get; set; }

        [Column("id_usluga")]
        public int? IdService { get; set; }  
        
        [Column("wariant")]
        public string? Variant { get; set; }

        [Column("liczba_godzin_teoria")]
        public int? NumberTheoryHours { get; set; }

        [Column("liczba_godzin_praktyka")]
        public int? NumberPraticeHours { get; set; }

        [Column("cena_brutto")]
        public decimal? Price { get; set; }

        [Column("teoria_zaliczona")]
        public bool? TheoryDone { get; set; }

        [Column("czy_opublikowane")]
        public bool? IsPublished { get; set; }

        [JsonIgnore]
        public Service? Service { get; set; }

        [JsonIgnore]
        public ICollection<TraineeCourse>? TraineeCourses { get; set; }

    }
}
