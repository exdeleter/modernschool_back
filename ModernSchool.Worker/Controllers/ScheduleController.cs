using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private SchoolDBContext db;
    public ScheduleController(SchoolDBContext db)
    {
        this.db = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Schedule>>> Get()
    {
        return await db.Schedules.Include(x=>x.Subject).ToListAsync();
    }
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Schedule>> Get(int studentID, DateTime date)
    // {
    //     var schedule = await (from s in db.Schedules
    //                           join st in db.Students on s.Class equals st.Class
    //                           where (st.Id == studentID && s.CurrentDate == date)
    //                           select s.Subjects).ToListAsync();
    //
    //     if (schedule == null )
    //     {
    //         return NotFound();
    //     }
    //     return new ObjectResult(schedule);
    // }
    
    [HttpGet("GetCurrentSchedule")]
    public async Task<ActionResult<Schedule>> CurrentGet(int studentID, DateOnly date)
    {
        var _studentSubject = await db.Students
            .Include(x => x.Subjects)
            .FirstOrDefaultAsync(x => x.Id == studentID);

        if (_studentSubject is null)
            return BadRequest("Student not found");

        var subjectId =  _studentSubject.Subjects.Select(x => x.Id).ToList();

        var schedule = await db.Schedules
            .Include(x => x.Problem)
            .Include(x => x.Subject)
            .Where(x => subjectId.Contains(x.Subject.Id))
            .Join(
                db.Marks.Include(x => x.Problem), 
                s => s.Subject.Id, 
                m => m.Problem.Id, 
                (s,m) =>  new
                {
                    s.Problem.Subject.Name,
                    s.Date,
                    s.Problem.Description,
                    m.Score,
                })
            .ToListAsync();

        if (schedule == null)
        {
            return NotFound();
        }
        
        return new ObjectResult(schedule);
    }
}