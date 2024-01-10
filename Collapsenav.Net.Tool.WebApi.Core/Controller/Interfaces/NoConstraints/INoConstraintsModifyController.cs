using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsModifyController<T, CreateT> : IController, IDisposable
{
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost, Route("")]
    Task<T?> AddAsync([FromBody] CreateT? entity);
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    Task DeleteAsync(string? id, [FromQuery] bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    Task UpdateAsync(string? id, CreateT? entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete, Route("ByIds")]
    Task<int> DeleteRangeAsync([FromQuery] IEnumerable<string>? id, [FromQuery] bool isTrue = false);
}
