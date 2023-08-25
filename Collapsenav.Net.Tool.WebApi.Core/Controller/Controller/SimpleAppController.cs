using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class SimpleAppController<T> : ControllerBase, ISimpleController<T>
    where T : class, IEntity
{
    private readonly ISimpleApplication<T> App;

    public SimpleAppController(ISimpleApplication<T> app)
    {
        App = app;
    }

    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost, Route("")]
    public virtual async Task<T?> AddAsync([FromBody] T? entity)
    {
        return await App.AddAsync(entity);
    }

    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string? id, [FromQuery] bool isTrue = false)
    {
        if (id == null)
            return;
        await App.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(string? id)
    {
        return await App.GetByIdAsync(id);
    }

    /// <summary>
    /// 修改
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(string id, [FromBody] T? entity)
    {
        await App.UpdateAsync(entity);
    }
}