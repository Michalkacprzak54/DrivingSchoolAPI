public class CourseDetailsDto
{
    public int IdCourseDetails { get; set; }
    public double TheoryHoursCount { get; set; }
    public double PraticeHoursCount { get; set; }
    public bool InternalExam { get; set; }
    public DateOnly CreationDate { get; set; }
    public string Notes { get; set; }
}
