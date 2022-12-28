using Microsoft.AspNetCore.Mvc;
using modernschool_back.Interfaces;
using modernschool_back.Models;

namespace modernschool_back.Controllers
{
    [Route("api/[controller]")]
    public class StudentController: Controller
    {
        IStudent _istudent;

        public StudentController(IStudent iStudent)
        {
            _istudent = iStudent;
            
        }

        [HttpGet(Name = "GetAllStudents")]
        public IEnumerable<Student> Get()
        {
            return _istudent.Get();
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult Get(int id)
        {
            Student stud = _istudent.Get(id);
            if(stud == null)
            {
                return NotFound();
            }
            return new ObjectResult(stud);
        }

        [HttpPost]
        public IActionResult Update(int Id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || updatedStudent.StudentId != Id)
            {
                return BadRequest();
            }

            var stud = _istudent.Get(Id);
            if (stud == null)
            {
                return NotFound();
            }

            _istudent.Update(updatedStudent);
            return RedirectToRoute("GetAllStudents");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedStudent = _istudent.Delete(Id);
            if (deletedStudent == null) 
            {
                return BadRequest();
            }

            return new ObjectResult(deletedStudent);
        }
    }
}
