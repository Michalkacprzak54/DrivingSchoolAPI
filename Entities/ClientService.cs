using System.ComponentModel.DataAnnotations.Schema;

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

        // Używaj właściwości Client i Service bez prefixów
        public Client Client { get; set; }
        public Service Service { get; set; }
    }
}
