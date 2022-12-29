using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Dtos;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    SchoolDBContext db;
    private readonly IMapper _mapper;
    
    public TestController(SchoolDBContext db, IMapper mapper)
    {
        this.db = db;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get()
    {
        return await db.Students.Include(x => x.Class)
            .Include(x => x.Class.Teacher).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> Get(int id)
    {
        Student student = await db.Students
            .Include(x => x.Class)
            .Include(x => x.Class.Teacher)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (student == null)
        {
            return NotFound();
        }
        var result =  _mapper.Map<StudentDto>(student);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student Student)
    {
        if (Student == null)
        {
            return BadRequest();
        }
        db.Students.Add(Student);
        await db.SaveChangesAsync();
        return Ok(Student);
    }

    [HttpPut]
    public async Task<ActionResult<Student>> Put(Student Student)
    {
        if (Student == null)
        {
            return BadRequest();
        }
        if (!db.Students.Any(x => x.Id == Student.Id)) 
        {
            return NotFound();
        }
        db.Update(Student);
        await db.SaveChangesAsync();
        return Ok(Student);
    }

    [HttpDelete]
    public async Task<ActionResult<Student>> Delete(int id)
    {
        Student Student = db.Students.FirstOrDefault(x => x.Id == id);
        if (Student == null)
        {
            return NotFound();
        }
        db.Students.Remove(Student);
        await db.SaveChangesAsync();
        return Ok(Student);
    }
}