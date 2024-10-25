using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("status")]
        public string State { get; set; }
        [Column("metoda_platnosci")]
        public string PaymentMethod { get; set; }
        [Column("opis_platnosci")]
        public string PaymentDescription { get; set; }

        public Invoice Invoice { get; set; }
    }
}
