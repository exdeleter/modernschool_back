using System.ComponentModel.DataAnnotations;

namespace ModernSchool.Worker.Models;

public class Class
{
    [Key]
    public int ClassId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public Teacher Teacher { get; set; }
}
