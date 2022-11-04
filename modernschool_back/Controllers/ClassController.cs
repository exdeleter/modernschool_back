using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernschool_back.Contexts;
using modernschool_back.Models;

namespace modernschool_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        SchoolDBContext db;
        public ClassController(SchoolDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            return await db.Class.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> Get(int id)
        {
            Class cls = await db.Class.FirstOrDefaultAsync(x => x.ClassId == id);
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
            if (!db.Class.Any(x => x.ClassId == cls.ClassId))
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
            Class cls = db.Class.FirstOrDefault(x => x.ClassId == id);
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
