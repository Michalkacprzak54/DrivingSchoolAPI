using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class ServiceSchedule
    {
        [Column("id_usluga_harmonogram")]
        public int IdServiceSchedule { get; set; }

        [Column("id_klient_usluga")]
        public int IdClientService { get; set; }

        [Column("id_harmonogram")]
        public int IdPraticeSchedule { get; set; }

        [Column("data_rezerwacji")]
        public DateTime ReservationDate { get; set; }

        [Column("data_wykonania")]
        public DateOnly? ExecutionDate { get; set; }

        [Column("godzina_rozpoczecia")]
        public TimeOnly? StartHour { get; set; }

        [Column("godzina_zakonczenia")]
        public TimeOnly? EndHour { get; set; }

        [Column("id_status")]
        public int IdStatus { get; set; }


        public ClientService? ClientService { get; set; }
        public PraticeSchedule? PraticeSchedule { get; set; }
        public Status? Status { get; set; }
    }
}
