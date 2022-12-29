using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Interfaces
{
    public interface IStudent
    {
        IEnumerable<Student> Get();
        Student Get(int id);
        void Create(Student student);
        void Update(Student student);
        Student Delete(int id);
    }
}
