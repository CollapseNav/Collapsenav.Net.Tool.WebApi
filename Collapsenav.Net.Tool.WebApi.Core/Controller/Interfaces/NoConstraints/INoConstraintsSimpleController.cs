using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsSimpleController<T> : INoConstraintsReadController<T>, IController
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(string? id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    Task<T?> AddAsync([FromBody] T? entity);
    /// <summary>
    /// 修改
    /// </summary>
    [HttpPut("{id}")]
    Task UpdateAsync(string id, [FromBody] T? entity);
}