using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Teacher : BaseEntity
{
    /// <summary>
    /// Имя 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия 
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }
    
    /// <summary>
    /// Возраст
    /// </summary>
    public int Age { get; set; }
    
    /// <summary>
    /// Предмет, который он приподает
    /// </summary>
    public Subject Subject { get; set; }
}
