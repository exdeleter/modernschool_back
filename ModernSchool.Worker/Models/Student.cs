using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public Class Class { get; set; }
    public string Phone { get; set; }
    public List<Subject> Subjects { get; set; } = new List<Subject>();  
}
