using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class QueryAppController<T, GetT> : ControllerBase, IQueryController<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected readonly IQueryApplication<T, GetT> App;
    public QueryAppController(IQueryApplication<T, GetT> app)
    {
        App = app;
    }
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(string? id)
    {
        return await App.GetByIdAsync(id);
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet, Route("")]
    public virtual async Task<PageData<T>> QueryPageAsync([FromQuery] GetT? input, [FromQuery] PageRequest? page = null) => await App.QueryPageAsync(input, page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    public virtual async Task<IEnumerable<T>> QueryAsync([FromQuery] GetT? input) => await App.QueryAsync(input);
    public virtual async Task<PageData<ReturnT>> QueryPageAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input, [FromQuery] PageRequest? page = null) where NewGetT : IBaseGet<T, ReturnT>
    {
        return await App.QueryPageAsync(input, page);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input) where NewGetT : IBaseGet<T, ReturnT>
    {
        return await App.QueryAsync(input);
    }
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<string>? ids) => await App.QueryByIdsAsync(ids);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<string>? ids) => await App.QueryByIdsAsync(ids);
}