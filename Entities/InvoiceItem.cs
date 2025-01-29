using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class InvoiceItem
    {
        [Column("id_pozycja")]
        public int IdInvoiceItem { get; set; }
        [Column("id_faktura")]
        public int IdInvocie { get; set; }
        [Column("id_klient_usluga")]
        public int IdClientService { get; set; }
        [Column("ilosc")]
        public int Quantity { get; set; }
        [Column("cena_jednostkowa")]
        public decimal UnitPrice { get; set; }
        [Column("kwota")]
        public decimal Price { get; set; }
        [Column("stawka_vat")]
        public int Vat { get; set; }

        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
