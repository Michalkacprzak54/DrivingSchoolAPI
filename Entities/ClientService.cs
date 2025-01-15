using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class ClientService
    {
        [Column("id_klient_usluga")]
        public int IdClientService { get; set; }

        [Column("id_klient")] 
        public int ClientId { get; set; }  

        [Column("id_usluga")] 
        public int? ServiceId { get; set; }

        [Column("id_usluga_wariant")]
        public int? VariantServiceId { get; set; }

        [Column("data_zakupu")]
        public DateTime PurchaseDate { get; set; }

        [Column("ilosc")]
        public int Quantity { get; set; }

        [Column("stan")]
        public string Status { get; set; }

        [Column("uwagi")]
        public string? Notes { get; set; }

        [Column("czy_wykorzystana")]
        public bool IsUsed { get; set; }

        [Column("ile_wykorzystane")]
        public int HowManyUsed{ get; set; }

        [Column("manual")]
        public bool? Manual { get; set; }

        [Column("automat")]
        public bool? Automatic { get; set; }

        // Używaj właściwości Client i Service bez prefixów
        public Client Client { get; set; }
        public Service Service { get; set; }
        public VariantService VariantService { get; set; }

        [JsonIgnore]
        public ServiceSchedule? ServiceSchedule { get; set; }   


    }
}
