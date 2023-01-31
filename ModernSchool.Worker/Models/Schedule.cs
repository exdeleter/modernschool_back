namespace ModernSchool.Worker.Models;

public class Schedule: BaseEntity
{
    /// <summary>
    /// Задание
    /// </summary>
    public Problem Problem { get; set; }
    
    /// <summary>
    /// Предмет
    /// </summary>
    public Subject Subject { get; set; }
    
    /// <summary>
    /// Дата, на которую задано домашнее задание
    /// </summary>
    public DateOnly Date { get; set; }
}