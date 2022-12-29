namespace ModernSchool.Worker.Models;

public class Student : BaseEntity
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
    public int Age { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public Class Class { get; set; }
    
    /// <summary>
    /// TODO
    /// </summary>
    public string Phone { get; set; }
    
    
    public List<Subject> Subjects { get; set; } = new List<Subject>();  
}
