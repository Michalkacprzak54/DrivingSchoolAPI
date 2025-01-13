using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class ClientService
    {
        [Column("id_klient_usluga")]
        public int IdClientService { get; set; }

        [Column("id_klient")] // To jest klucz obcy do Client
        public int ClientId { get; set; }  // Użyj 'ClientId' zamiast 'ClientServiceIdClient'

        [Column("id_usluga")] // To jest klucz obcy do Service
        public int ServiceId { get; set; }  // Użyj 'ServiceId' zamiast 'ClientServiceIdService'

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

        [Column("podstawowa_praktyka")]
        public bool? BasicPractice { get; set; }

        [Column("rozszerzona_praktyka")]
        public bool? ExtendedPractice { get; set; }

        [Column("online_teoria")]
        public bool? OnlineTheory { get; set; }

        [Column("stacjonarnie_teoria")]
        public bool? StationaryTheory { get; set; }

        [Column("zaliczona_teoria")]
        public bool? TheoryCompleted { get; set; }

        [Column("manual")]
        public bool? Manual { get; set; }

        [Column("automat")]
        public bool? Automatic { get; set; }

        // Używaj właściwości Client i Service bez prefixów
        public Client Client { get; set; }
        public Service Service { get; set; }

        [JsonIgnore]
        public ServiceSchedule? ServiceSchedule { get; set; }   


    }
}
