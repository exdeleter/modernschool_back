using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Contexts;

public class SchoolDBContext : DbContext
{
    public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Class> Class { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Mark>Marks { get; set; }
    
    public DbSet<Problem>Problems { get; set; }
    
    public DbSet<Term>Terms { get; set; }
    
    public DbSet<Schedule>Schedules { get; set; }

    public DbSet<User> SchoolUsers { get; set; }
}