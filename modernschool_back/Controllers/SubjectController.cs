using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernschool_back.Contexts;
using modernschool_back.Models;

namespace modernschool_back.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        SchoolDBContext db;
        public SubjectController(SchoolDBContext db)
        {
            this.db = db;
            if (!db.Subjects.Any())
            {
                db.Subjects.Add(new Subject { Name = "Философия" });
                db.Subjects.Add(new Subject { Name = "УМФ" });
                db.Subjects.Add(new Subject { Name = "Русский язык" });
                db.Subjects.Add(new Subject { Name = "Математика" });
                db.Subjects.Add(new Subject { Name = "Обществознание" });
                db.Subjects.Add(new Subject { Name = "Физкультура" });
                db.SaveChanges();
            }
        }
        

        [HttpGet(Name = "GetAllSubjects"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Subject>>> Get()
        {
            return await db.Subjects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>>Get(int id)
        {
            Subject subject = await db.Subjects.FirstOrDefaultAsync(x => x.SubjectId == id);
            if (subject == null) 
            {
                return NotFound();
            }
            return new ObjectResult(subject);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>>Post(Subject subject)
        {
            if (subject == null) 
            {
                return BadRequest();
            }
            db.Subjects.Add(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }

        [HttpPut]
        public async Task<ActionResult<Subject>> Put(Subject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            if (!db.Subjects.Any(x => x.SubjectId == subject.SubjectId))
            {
                return NotFound();
            }
            db.Update(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }

        [HttpDelete]
        public async Task<ActionResult<Subject>> Delete(int id)
        {
            Subject subject = db.Subjects.FirstOrDefault(x => x.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }
    }
}
