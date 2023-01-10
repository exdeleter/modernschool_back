namespace ModernSchool.Worker.Models;

public class Term : BaseEntity
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Начало
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Конец
    /// </summary>
    public DateTime EndDate { get; set; }
    
    /// <summary>
    /// Год
    /// </summary>
    public int CurrentYear { get; set; }
}