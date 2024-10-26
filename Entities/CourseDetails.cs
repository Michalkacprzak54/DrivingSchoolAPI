using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class CourseDetails
    {
        [Column("id_szczegoly")]
        public int IdCourseDetails { get; set; }
        [Column("id_kursant_kurs")]
        public int IdTraineeCourse { get; set; }
        [Column("liczba_godzin_teoria")]
        public double TheoryHoursCount { get; set; }
        [Column("liczba_godzin_praktyka")]
        public double PraticeHoursCount { get; set; }
        [Column("egzamin_wewnetrzny")]
        public bool InternalExam { get; set; }
        [Column("data_utworzenia")]
        public DateOnly CreationDate { get; set; }
        [Column("uwagi")]
        public string Notes { get; set; }

        public TraineeCourse TraineeCourse { get; set; }
  
    }
}
