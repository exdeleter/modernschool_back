using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernschool_back.Contexts;
using modernschool_back.Models;

namespace modernschool_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        SchoolDBContext db;
        public TeacherController(SchoolDBContext db)
        {
            this.db = db;
            if (!db.Teachers.Any())
            {
                db.Teachers.Add(new Teacher
                {
                    Name = "Маргарита", Surname = "Алешина", Patronymic = "Серафимовна",
                    Age = 37, Subject = new Subject { Name = "Базы данных" }
                });
                db.Teachers.Add(new Teacher
                {
                    Name = "Клавдия", Surname = "Иванцова", Patronymic = "Николаевна",
                    Age = 40, Subject = new Subject { Name = "Программирование" }
                });
                db.Teachers.Add(new Teacher
                {
                    Name = "Герман", Surname = "Кошков", Patronymic = "Никитович",
                    Age = 42, Subject = new Subject { Name = "Музыка" }
                });
                db.Teachers.Add(new Teacher
                {
                    Name = "Давид", Surname = "Халимдаров", Patronymic = "Григорьевич",
                    Age = 28, Subject = new Subject { Name = "Литература" }
                });
                db.Teachers.Add(new Teacher
                {
                    Name = "Анна", Surname = "Балашова", Patronymic = "Степановна",
                    Age = 29, Subject = new Subject { Name = "Английский" }
                });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            return await db.Teachers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            Teacher teacher = await db.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return new ObjectResult(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> Post(Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            db.Teachers.Add(teacher);
            await db.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpPut]
        public async Task<ActionResult<Teacher>> Put(Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            if (!db.Teachers.Any(x => x.TeacherId == teacher.TeacherId)) 
            {
                return NotFound();
            }
            db.Update(teacher);
            await db.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpDelete]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(x => x.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();
            return Ok(teacher);
        }
    }
}
