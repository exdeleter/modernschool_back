namespace ModernSchool.Worker.Models
{
    public class User: BaseEntity
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
