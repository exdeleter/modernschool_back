namespace ModernSchool.Worker.Models;

/// <summary>
/// Предмет
/// </summary>
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
    
    /// <summary>
    /// Ведущий преподаватель
    /// </summary>
    /// <returns></returns>
    public Teacher Teacher { get; set; }
    
    /// <summary>
    /// Список студентов
    /// </summary>
    public List<Student> Students { get; set; }
}
