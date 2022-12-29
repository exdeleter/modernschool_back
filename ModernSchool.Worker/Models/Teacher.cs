using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Teacher
{
    [Key]
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public Subject Subject { get; set; }
}
