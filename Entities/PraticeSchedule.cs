using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
