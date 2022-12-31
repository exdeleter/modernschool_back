namespace ModernSchool.Worker.Models;

public class Class : BaseEntity
{
    /// <summary>
    /// Наименование 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Номер 
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Учитель, привязанный к классу (руководитель)
    /// </summary>
    public Teacher Teacher { get; set; }
}
