using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace DrivingSchoolAPI.Entities
{
    public class Admin
    {
        [Column("id_administrator")]
        public int IdAdmin { get; set; }

        [Column("haslo")]
        public string AdminPassword { get; set; }
        
        [Column("email")]
        public string AdminEmail { get; set; }
        [Column("telefon")]
        public string AdminPhoneNumber { get; set; }

        [Column("data_utworzenia")]
        public DateTime CreationDate { get; set; }

        [Column("czy_aktwyny")]
        public bool IsActive { get; set; }
    }


}
