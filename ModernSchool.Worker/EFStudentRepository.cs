using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Interfaces;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker
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
            if (!context.Students.Any())
            {
                context.Students.Add(new Student
                {
                    Name = "Антон",
                    Surname = "Максимушкин",
                    Patronymic = "Аркадинович",
                    Age = 7,
                    Phone = "8902333434",
                    Class = new Class
                    {
                        Name = "В", Number = 2, Teacher = new Teacher
                        {
                            Name = "Руслан", Surname = "Сиянковский", Patronymic = "Артемович",
                            Age = 32, Subject = new Subject { Name = "Физкультура" }
                        }
                    },
                    Subjects = new List<Subject>() { new Subject { Name = "БД" }, new Subject { Name = "Русский язк" } }
                });
                context.SaveChanges();
            }
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
