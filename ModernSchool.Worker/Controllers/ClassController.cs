using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : BaseController<Class>
    {
        public ClassController(SchoolDBContext context) : base(context)
        {

        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            return await _dbSet.Include(x=>x.Teacher).ToListAsync();
        }
    }
}
