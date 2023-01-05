namespace ModernSchool.Worker.Models;

public class Task: BaseEntity
{
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дедлайн
    /// </summary>
    public DateTime DeadLine { get; set; }
    
    /// <summary>
    /// Предмет
    /// </summary>
    public Subject Subject { get; set; }
}