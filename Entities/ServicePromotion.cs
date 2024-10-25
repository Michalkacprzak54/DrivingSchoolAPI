using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class ServicePromotion
    {
        [Column("id_usluga_promocja")]
        public int IdServicePromotion { get; set; }
        [Column("id_usluga")]
        public int IdService { get; set; }
        [Column("id_promocja")]
        public int IdPromotion { get; set; }
        [Column("data_przypisania")]
        public DateOnly AssigmentDate { get; set; }

        public Promotion Promotion { get; set; }
        public Service Service { get; set; }
    }
}
