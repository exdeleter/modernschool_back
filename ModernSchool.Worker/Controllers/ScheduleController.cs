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
    public async Task<ActionResult<Schedule>> CurrentGet(int studentID, DateTime date)
    {
        
        var _studentSubject = await db.Students
            .Include(x => x.Subjects)
            .Where(x => x.Id == studentID)
            .Select(x => x.Subjects)
            .ToListAsync();

        var test = new List<int>();
        
        foreach (var subject in _studentSubject)
        {
            foreach (var underSubject in subject)
            {
                test.Add(underSubject.Id);
            }
        }
        
        var schedule = await db.Schedules
            .Include(x => x.Problem)
            .Include(x => x.Problem.Subject)
            .Include(x => x.Subject)
            .Where(x => test.Contains(x.Subject.Id))
            .Join(
                db.Marks.Include(x => x.Problem)
                    .Include(x => x.Problem.Subject), 
                s => s.Subject.Id, 
                m => m.Problem.Subject.Id, 
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