using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Subject
{
    [Key]
    public int SubjectId { get; set; }
    public string Name { get; set; }
}
