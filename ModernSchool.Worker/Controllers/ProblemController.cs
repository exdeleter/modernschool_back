using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : BaseController<Problem>
    {
        public ProblemController(SchoolDBContext context) : base(context)
        {

        }
        
        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Problem>>> GetByDateStudentId(int studentID, DateOnly date)
        {
            // var subjectIds = await _context.SubjectStudents
            //     .Include(x => x.Student)
            //     .Include(x=> x.Subject)
            //     .Where(x => x.Student.Id == studentID)
            //     .Select(x => x.Subject.Id)
            //     .ToListAsync();
            
            return await _dbSet
                .Include(x => x.Subject)
                .Where(x => x.DeadLine == date && 
                            new List<int>{1,2,3}.Contains(x.Subject.Id))
                .ToListAsync();
        }
        
        
    }
}
