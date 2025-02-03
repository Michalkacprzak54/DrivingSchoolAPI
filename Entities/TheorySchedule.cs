using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class TheorySchedule
    {
        [Column("id_harmonogram_wyklad")]
        public int IdTheorySchedule { get; set; }
        [Column("id_instruktor")]
        public int? IdInsctructor { get; set; }
        [Column("grupa")]
        public string? GroupName { get; set; }
        [Column("data")]
        public DateOnly? Date { get; set; }
        [Column("dzien_tygodnia")]
        public string? DayName { get; set; }
        [Column("godzina_rozpoczecia")]
        public TimeOnly? StartHour { get; set; }
        [Column("godzina_zakonczenia")]
        public TimeOnly? EndHour { get; set; }

        public Instructor? Instructor { get; set; }
    }
}
