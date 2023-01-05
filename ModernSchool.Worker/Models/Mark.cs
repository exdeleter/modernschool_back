namespace ModernSchool.Worker.Models;

public class Mark: BaseEntity
{
    /// <summary>
    /// Задание
    /// </summary>
    public Task Task { get; set; }
    
    /// <summary>
    /// Оценка
    /// </summary>
    public decimal Score { get; set; }
}