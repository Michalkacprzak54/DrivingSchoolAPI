using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class TraineeCourse
    {
        [Column("id_kursant_kurs")]
        public int IdTraineeCourse { get; set; }
        [Column("id_klient")]
        public int IdClient{ get; set; }
        [Column("id_usluga")]
        public int IdService { get; set; }
        [Column("data_rozpoczecia")]
        public DateOnly StartDate { get; set; }
        [Column("data_zakonczenia")]
        public DateOnly EndDate { get; set; }
        [Column("id_status")]
        public int IdStatus { get; set; }
        [Column("PESEL")]
        public decimal PESEL { get; set; }
        [Column("pkk")]
        public string PKK { get; set; }
        [Column("badania_lekarskie")]
        public bool MedicalCheck { get; set; }

        [Column("uwagi")]
        public string? Notes { get; set; }

        public Client Client { get; set; }
        public Service Service { get; set; }
        public Status Status { get; set; }
        public CourseDetails CourseDetails { get; set; }

    }
}
