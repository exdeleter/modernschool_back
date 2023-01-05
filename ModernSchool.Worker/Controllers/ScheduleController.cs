using Microsoft.AspNetCore.Mvc;

namespace ModernSchool.Worker.Controllers;

public class ScheduleController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}