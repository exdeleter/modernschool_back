using System.ComponentModel.DataAnnotations.Schema;

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
    [ForeignKey("TeacherId")]
    public Teacher? Teacher { get; set; }
    
    /// <summary>
    /// Идентификатор учителя
    /// </summary>
    public int? TeacherId { get; set; }

    /// <summary>
    /// Ученики
    /// </summary>
    public List<Student>? Students { get; set; } = new List<Student>();
}
