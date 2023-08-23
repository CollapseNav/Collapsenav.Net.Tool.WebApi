using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class SimpleReqController<T> : ControllerBase, ISimpleController<T>
    where T : class, IEntity
{
    private readonly ICrudRepository<T> repository;

    public SimpleReqController(ICrudRepository<T> repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    public virtual async Task<T?> AddAsync([FromBody] T? entity)
    {
        return await repository.AddAsync(entity);
    }

    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string? id, [FromQuery] bool isTrue = false)
    {
        if (id == null)
            return;
        await repository.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(string? id)
    {
        return await repository.GetByIdAsync(id);
    }

    /// <summary>
    /// 修改
    /// </summary>
    [HttpPut("{id}")]
    public virtual async Task UpdateAsync(string id, [FromBody] T? entity)
    {
        await repository.UpdateAsync(entity);
    }
}