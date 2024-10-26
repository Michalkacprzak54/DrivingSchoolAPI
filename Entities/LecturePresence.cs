using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class LecturePresence
    {
        [Column("id_obecnosc")]
        public int IdLecturePresence { get; set; }
        [Column("id_szczegoly")]
        public int IdCourseDetails { get; set; }
        [Column("id_harmonogram_wyklad")]
        public int IdTheorySchedule { get; set; }
        [Column("data_obecnosci")]
        public DateOnly PresanceDate { get; set; }
    }
}
