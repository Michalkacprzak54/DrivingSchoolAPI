public class ScheduleRequest
{
    public int InstructorId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Group { get; set; }
    public string Type { get; set; }
}
