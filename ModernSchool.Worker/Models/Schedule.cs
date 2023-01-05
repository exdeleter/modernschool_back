namespace ModernSchool.Worker.Models;

public class Schedule: BaseEntity
{
    public List<Task> TasksForDay { get; set; }
    public List<Subject>Subjects { get; set; }
    public DateTime CurrentDate { get; set; }
    public Class Class { get; set; }
}