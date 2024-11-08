using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class ClientLogin
    {

        [Column("id_login_klient")]
        public int IdClientLogin { get; set; }
        [Column("id_klient")]
        public int IdClient{ get; set; }
        [Column("adres_email")]
        public string ClientEmail { get; set; }
        [Column("haslo")]
        public string ClientPassword { get; set; }


    }
}
