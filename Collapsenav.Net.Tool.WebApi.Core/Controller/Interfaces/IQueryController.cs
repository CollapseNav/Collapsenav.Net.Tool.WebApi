using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryController<T, GetT> : INoConstraintsQueryController<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 带条件分页
    /// </summary>
    Task<PageData<ReturnT>> QueryPageAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input, [FromQuery] PageRequest? page = null) where NewGetT : IBaseGet<T, ReturnT>;
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>([FromQuery] NewGetT? input) where NewGetT : IBaseGet<T, ReturnT>;
}

