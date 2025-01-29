using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class Payment
    {
        [Column("id_platnosc")]
        public int IdPayment { get; set; }

        [Column("id_faktura")]
        public int IdInvoice { get; set; }

        [Column("data_platnosci")]
        public DateTime PaymentDate { get; set; }

        [Column("kwota")]
        public decimal AmountPaid { get; set; }


        [Column("metoda_platnosci")]
        public string PaymentMethod { get; set; }


        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
