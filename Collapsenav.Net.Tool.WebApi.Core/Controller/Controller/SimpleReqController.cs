using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class SimpleReqController<T> : ControllerBase, ISimpleController<T>
    where T : class, IEntity
{
    private readonly ICrudRepository<T> Repository;
    public SimpleReqController(ICrudRepository<T> repository)
    {
        this.Repository = repository;
    }
    [HttpPost, Route("")]
    public virtual async Task<T?> AddAsync([FromBody] T? entity)
    {
        return await Repository.AddAsync(entity);
    }
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string? id, [FromQuery] bool isTrue = false)
    {
        if (id == null)
            return;
        await Repository.DeleteAsync(id, isTrue);
    }
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(string? id)
    {
        return await Repository.GetByIdAsync(id);
    }
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(string id, [FromBody] T? entity)
    {
        await Repository.UpdateAsync(entity);
    }
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(object? id)
    {
        return await Repository.GetByIdAsync(id);
    }
}