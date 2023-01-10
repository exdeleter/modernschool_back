namespace ModernSchool.Worker.Models;

public class Subject : BaseEntity
{
    /// <summary>
    /// Наименование предмета
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Семестр
    /// </summary>
    public Term Term { get; set; }
    
}
