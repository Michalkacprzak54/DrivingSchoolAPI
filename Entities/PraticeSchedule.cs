using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DrivingSchoolAPI.Entities
{
    public class PraticeSchedule
    {
        [Column("id_harmonogram_praktyka")]
        public int IdPraticeSchedule { get; set; }    
        [Column("id_instruktor")]
        public int IdInstructor { get; set; }    
        [Column("data")]
        public DateOnly Date { get; set; }    
        [Column("dzien_tygodnia")]
        public string DayName { get; set; }    
        [Column("godzina_rozpoczęcia")]
        public TimeOnly StartDate{ get; set; }    
        [Column("godzina_zakończenia")]
        public TimeOnly EndDate{ get; set; }

        [Column("czy_aktualne")]
        public bool is_Available { get; set; }

        [Column("czy_skonczone")]
        public bool isDone { get; set; }
        public Instructor? Instructor { get; set; }

        [JsonIgnore]
        public ServiceSchedule ServiceSchedule { get; set; }

    }
}
