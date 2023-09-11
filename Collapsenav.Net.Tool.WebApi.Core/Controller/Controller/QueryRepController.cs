using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
namespace Collapsenav.Net.Tool.WebApi;

[ApiController]
[Route("[controller]")]
public class QueryRepController<T, GetT> : ControllerBase, IQueryController<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected readonly IQueryRepository<T> Repository;
    public QueryRepController(IQueryRepository<T> repository) : base()
    {
        Repository = repository;
    }
    [HttpGet, Route("{id}")]
    public virtual async Task<T?> QueryAsync(string? id)
    {
        return await Repository.GetByIdAsync(id);
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet, Route("")]
    public virtual async Task<PageData<T>> QueryPageAsync([FromQuery] GetT? input, [FromQuery] PageRequest? page = null) => await Repository.QueryPageAsync(input?.GetQuery(Repository), page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    public virtual async Task<IEnumerable<T>> QueryAsync([FromQuery] GetT? input) => await Repository.QueryAsync(input?.GetQuery(Repository));
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<string>? ids) => await Repository.QueryByIdsAsync(ids);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<string>? ids) => await Repository.QueryByIdsAsync(ids);

    public virtual async Task<PageData<ReturnT>> QueryPageAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input, [FromQuery] PageRequest? page = null) where NewGetT : IBaseGet<T, ReturnT>
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input) where NewGetT : IBaseGet<T, ReturnT>
    {
        throw new NotImplementedException();
    }
}