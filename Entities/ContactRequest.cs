using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class ContactRequest
    {
        [Column("id_zgloszenie_kontakt")]
        public int IdContactRequest { get; set; }
        
        [Column("imie")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("numer_telefonu")]
        public string? Phone { get; set; }

        [Column("tresc_wiadomosci")]
        public string Message { get; set; }
    }
}
