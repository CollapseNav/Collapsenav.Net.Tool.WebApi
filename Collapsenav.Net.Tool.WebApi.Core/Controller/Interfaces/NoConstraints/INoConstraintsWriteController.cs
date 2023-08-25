using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsWriteController<T, CreateT> : IController, IDisposable
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(string? id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost, Route("")]
    Task<T?> AddAsync([FromBody] CreateT? entity);
}
public interface INoConstraintsWriteController<TKey, T, CreateT> : INoConstraintsWriteController<T, CreateT>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(TKey? id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    Task UpdateAsync(TKey? id, CreateT? entity);
}
