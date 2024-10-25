using System.ComponentModel.DataAnnotations.Schema;


namespace DrivingSchoolAPI.Entities
{
    public class Invoice
    {
        [Column("id_faktura")]
        public int IdInvocie { get; set; }
        [Column("id_klient")]
        public int IdClient { get; set; }
        [Column("numer_faktura")]
        public string InvocieNumber { get; set; }
        [Column("data_wystawienia")]
        public DateTime IssueDate { get; set; }
        [Column("data_platnosci")]
        public DateTime? PaymentDate { get; set; }
        [Column("kwota_calkowita")]
        public decimal FullAmount { get; set; }
        [Column("stan")]
        public string InvoiceState { get; set; }
        public Client Client { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<InvoiceItem> InvoviceItems { get; set; }  
    }
}
