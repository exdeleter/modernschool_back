using modernschool_back.Models;

namespace modernschool_back
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
