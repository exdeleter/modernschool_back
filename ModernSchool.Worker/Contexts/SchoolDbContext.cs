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
}