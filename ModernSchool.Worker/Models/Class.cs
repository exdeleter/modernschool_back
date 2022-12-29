namespace ModernSchool.Worker.Models;

public class Class : BaseEntity
{
    /// <summary>
    /// Наименование класса
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Цифра
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Teacher Teacher { get; set; }
}
