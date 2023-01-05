using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : BaseController<Schedule>
{
    public ScheduleController(DbContext context) : base(context)
    {
        
    }
}