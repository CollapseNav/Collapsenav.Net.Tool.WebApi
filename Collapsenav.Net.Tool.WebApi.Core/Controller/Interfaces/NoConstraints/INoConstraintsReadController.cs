using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsReadController<T> : IController
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T?> QueryAsync(string? id);
}
public interface INoConstraintsReadController<TKey, T> : INoConstraintsReadController<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T?> QueryAsync(TKey? id);
}