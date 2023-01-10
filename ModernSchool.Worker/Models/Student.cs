namespace ModernSchool.Worker.Models;

public class Student : BaseEntity
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
    /// Класс студента
    /// </summary>
    public Class Class { get; set; }
    
    /// <summary>
    /// Телефон
    /// </summary>
    public string Phone { get; set; }
}
