using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class Pratice
    {
        [Column("id_praktyka")]
        public int IdPratice { get; set; }
        [Column("id_harmonogram")]
        public int IdPraticeSchedule { get; set; }
        [Column("id_szczegoly")]
        public int IdCourseDetails { get; set; }
        [Column("data_rezerwacji")]
        public DateTime? ReservationDate { get; set; }
        [Column("data_praktyk")]
        public DateOnly? PraticeDate { get; set; }
        [Column("godzina_rozpoczecia")]
        public TimeOnly? StartHour { get; set; }
        [Column("godzina_zakonczenia")]
        public TimeOnly? EndHour { get; set; }
        [Column("id_status")]
        public int? IdStatus { get; set; }

    }
}
