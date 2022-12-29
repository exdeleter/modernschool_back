using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        SchoolDBContext db;
        public ClassController(SchoolDBContext db)
        {
            this.db = db;
            if (!db.Class.Any())
            {
                db.Class.Add(new Class
                {
                    Name = "A", Number = 1, Teacher = new Teacher
                    {
                        Name = "Маргарита", Surname = "Алешина", Patronymic = "Серафимовна",
                        Age = 37, Subject = new Subject { Name = "Базы данных" }
                    }
                });
                db.Class.Add(new Class
                {
                    Name = "A", Number = 2, Teacher = new Teacher
                    {
                        Name = "Александр", Surname = "Ярошевский", Patronymic = "Филиппович",
                        Age = 38, Subject = new Subject { Name = "ИЗО" }
                    }
                });
                db.Class.Add(new Class
                {
                    Name = "Б", Number = 1, Teacher = new Teacher
                    {
                        Name = "Любовь", Surname = "Унгерна", Patronymic = "Васильевна",
                        Age = 42, Subject = new Subject { Name = "ОБЖ" }
                    }
                });
                db.Class.Add(new Class
                {
                    Name = "В", Number = 1, Teacher = new Teacher
                    {
                        Name = "Павел", Surname = "Сиянковский", Patronymic = "Артемович",
                        Age = 32, Subject = new Subject { Name = "Физкультура" }
                    }
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            return await db.Class.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> Get(int id)
        {
            Class cls = await db.Class.FirstOrDefaultAsync(x => x.Id == id);
            if (cls == null)
                return NotFound();
            return new ObjectResult(cls);
        }

        
        [HttpPost]
        public async Task<ActionResult<Class>> Post(Class cls)
        {
            if (cls == null)
            {
                return BadRequest();
            }

            db.Class.Add(cls);
            await db.SaveChangesAsync();
            return Ok(cls);
        }

        
        [HttpPut]
        public async Task<ActionResult<Class>> Put(Class cls)
        {
            if (cls == null)
            {
                return BadRequest();
            }
            if (!db.Class.Any(x => x.Id == cls.Id))
            {
                return NotFound();
            }

            db.Update(cls);
            await db.SaveChangesAsync();
            return Ok(cls);
        }

 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> Delete(int id)
        {
            Class cls = db.Class.FirstOrDefault(x => x.Id == id);
            if (cls == null)
            {
                return NotFound();
            }
            db.Class.Remove(cls);
            await db.SaveChangesAsync();
            return Ok(cls);
        }
    }
}
