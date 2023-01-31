namespace ModernSchool.Worker.Models;

/// <summary>
/// Задание
/// </summary>
public class Problem: BaseEntity
{
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дедлайн
    /// </summary>
    public DateOnly DeadLine { get; set; }
    
    /// <summary>
    /// Предмет
    /// </summary>
    public Subject Subject { get; set; }
}