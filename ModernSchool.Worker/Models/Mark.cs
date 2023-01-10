namespace ModernSchool.Worker.Models;

public class Mark: BaseEntity
{
    /// <summary>
    /// Задание
    /// </summary>
    public Problem Problem { get; set; }
    
    /// <summary>
    /// Оценка
    /// </summary>
    public int Score { get; set; }
}