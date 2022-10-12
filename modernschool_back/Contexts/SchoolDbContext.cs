using Microsoft.EntityFrameworkCore;
using modernschool_back.Models;

namespace modernschool_back.Contexts
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }    
        public DbSet<Class> Class { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }    
    }
}
