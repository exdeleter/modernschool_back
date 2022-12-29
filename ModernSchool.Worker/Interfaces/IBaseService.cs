using Microsoft.AspNetCore.Mvc;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Interfaces;

public interface IBaseService<T>
    where T : BaseEntity
{
    Task<ActionResult<IEnumerable<T>>> Get();
    
    Task<ActionResult<T>> Get(int? id);
    
    Task<ActionResult<T>> Post(T? entity);
    
    Task<ActionResult<T>> Put (T entity);
    
    Task<ActionResult<T>> Delete(int id);
}