using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
[ApiController]
[Route("[controller]")]
public class ModifyAppController<T, CreateT> : ControllerBase, IModifyController<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected readonly IModifyApplication<T, CreateT> App;
    protected readonly IMap Mapper;
    public ModifyAppController(IModifyApplication<T, CreateT> app, IMap mapper)
    {
        App = app;
        Mapper = mapper;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost, Route("")]
    public virtual async Task<T?> AddAsync([FromBody] CreateT? entity) => await App.AddAsync(entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys) => await App.AddRangeAsync(entitys);
    [NonAction]
    public void Dispose() => App.Dispose();
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string? id, [FromQuery] bool isTrue = false) => await App.DeleteAsync(id, isTrue);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete, Route("")]
    public virtual async Task<int> DeleteRangeAsync([FromQuery] IEnumerable<string>? id, [FromQuery] bool isTrue = false) => await App.DeleteRangeAsync(id, isTrue);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(string? id, CreateT? entity) => await App.UpdateAsync(id, entity);
}