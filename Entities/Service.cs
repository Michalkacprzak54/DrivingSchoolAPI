using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class Service
    {
        [Column("id_usluga")]
        public int IdService { get; set; }

        [Column("nazwa_usluga")]
        public string ServiceName { get; set; }
        [Column("opis")]
        public string ServiceDescription { get; set; }
        [Column("cena_brutto")]
        public decimal ServicePrice { get; set; }

        [Column("typ_usluga")]
        public string ServiceType { get; set; }

        [Column("miejsce_usluga")]
        public string ServicePlace { get; set; }

        [Column("kategoria")]
        public string? Category { get; set; }

        public TraineeCourse TraineeCourse { get; set; }

        public ICollection<VariantService> VariantServices { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
