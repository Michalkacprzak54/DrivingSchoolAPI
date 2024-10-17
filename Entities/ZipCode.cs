using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class ZipCode
    {
        [Column("id_kod_pocztowy")]
        public int IdZipCode { get; set; }
        [Column("kod_pocztowy")]
        public string ZipCodeNumber { get; set; }
    }
}
