using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsQueryController<T, GetT> : IController
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    Task<T?> QueryAsync(string? id);
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet, Route("")]
    Task<PageData<T>> QueryPageAsync([FromQuery] GetT? input, [FromQuery] PageRequest? page = null);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    Task<IEnumerable<T>> QueryAsync([FromQuery] GetT? input);
    // /// <summary>
    // /// 带条件分页
    // /// </summary>
    // Task<PageData<ReturnT>> QueryPageAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input, [FromQuery] PageRequest? page = null);
    // /// <summary>
    // /// 带条件查询(不分页)
    // /// </summary>
    // Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<string>? ids);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<string>? ids);
}