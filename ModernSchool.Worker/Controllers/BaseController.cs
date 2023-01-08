using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Interfaces;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers;

public class BaseController<TEntity> 
    : ControllerBase, IBaseService<TEntity>
    where TEntity : BaseEntity
{
    private  DbContext _context { get; set; }

    protected DbSet<TEntity> _dbSet { get; set; }
    
    public BaseController(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> Get(int id)
    {
        TEntity? result = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TEntity>> Post(TEntity? entity)
    {
        if (entity is null)
            return BadRequest();

        await _dbSet.AddAsync(entity);

        return Ok(entity);
    }
    
    [HttpPut]
    public async Task<ActionResult<TEntity>> Put(TEntity? entity)
    {
        if (entity is null)
            return BadRequest();

        if (!_dbSet.Any(x => x.Id == entity.Id))
            return NotFound();

        _dbSet.Update(entity);

        await _context.SaveChangesAsync();

        return Ok(entity);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<TEntity>> Delete(int id)
    {
        TEntity? entity = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null)
        {
            return NotFound();
        }
        
        _dbSet.Remove(entity);
        
        await _context.SaveChangesAsync();
        
        return Ok(entity);
    }
}