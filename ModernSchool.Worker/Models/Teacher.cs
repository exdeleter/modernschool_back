using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Teacher : BaseEntity
{
    /// <summary>
    /// TODO
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public string Patronymic { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public int Age { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public Subject Subject { get; set; }
}
