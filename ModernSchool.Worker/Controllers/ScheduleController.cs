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
        return await db.Schedules.Include(x=>x.Subjects).Include(x=>x.Class).ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Schedule>> Get(int studentID, DateTime date)
    {
        var schedule = await (from s in db.Schedules
                              join st in db.Students on s.Class equals st.Class
                              where (st.Id == studentID && s.CurrentDate == date)
                              select s.Subjects).ToListAsync();

        if (schedule == null )
        {
            return NotFound();
        }
        return new ObjectResult(schedule);
    }
}