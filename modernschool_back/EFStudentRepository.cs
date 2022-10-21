using modernschool_back.Contexts;
using modernschool_back.Interfaces;
using modernschool_back.Models;

namespace modernschool_back
{
    public class EFStudentRepository: IStudent
    {
        private SchoolDBContext _context;
        public IEnumerable<Student> Get()
        {
            return _context.Students;
        }
        public Student Get(int id)
        {
            return _context.Students.Find(id);
        }
        public EFStudentRepository(SchoolDBContext context)
        {
            _context = context;
        }
        public void Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void Update(Student updatedStudent)
        {
            Student currentStudent = Get(updatedStudent.StudentId);
            currentStudent.Name = updatedStudent.Name;
            currentStudent.Age = updatedStudent.Age;
            currentStudent.Surname = updatedStudent.Surname;
            currentStudent.Patronymic = updatedStudent.Patronymic;
            currentStudent.Subjects = updatedStudent.Subjects;
            currentStudent.Class = updatedStudent.Class;
            currentStudent.Phone = updatedStudent.Phone;
            currentStudent.StudentId = updatedStudent.StudentId;

            _context.Students.Update(currentStudent);
            _context.SaveChanges();
        }

        public Student Delete(int id)
        {
            Student student = Get(id);

            if (student != null) 
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }
    }
}
