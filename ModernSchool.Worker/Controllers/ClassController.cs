using Microsoft.AspNetCore.Http;
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
        public ClassController(DbContext context) : base(context)
        {
        }
    }
}
