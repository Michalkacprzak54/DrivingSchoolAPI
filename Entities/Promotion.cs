using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class Promotion
    {
        [Column("id_promocja")]
        public int IdPromotion { get; set; }    
        [Column("nazwa_promocja")]
        public string PromotionName { get; set; }
        [Column("wartosc_promocja")]
        public decimal PromotionValue { get; set; }
        [Column("opis_promocja")]
        public string PromotionDescription { get; set; }
        [Column("aktywna")]
        public bool IsActive { get; set; }

        public ServicePromotion ServicePromotion { get; set; }

    }
}
