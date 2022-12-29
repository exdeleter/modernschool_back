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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TEntity>>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TEntity>> Get(int? id)
    {
        if (id is null)
            return BadRequest();

        TEntity? result = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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