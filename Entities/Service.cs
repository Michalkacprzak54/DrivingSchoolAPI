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
        [Column("cena_netto")]
        public decimal ServiceNetPrice { get; set; }
        [Column("stawka_vat")]
        public int ServiceVatRate { get; set; }
        [Column("typ_usluga")]
        public string ServiceType { get; set; }

        [Column("miejsce_usluga")]
        public string ServicePlace { get; set; }

        public TraineeCourse TraineeCourse { get; set; }
        public ServicePromotion ServicePromotion { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
