using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsQueryApplication<T, GetT> : IApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T?> GetByIdAsync<TKey>(TKey? id);
    /// <summary>
    /// 获取query
    /// </summary>
    IQueryable<T> GetQuery(GetT? input);
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PageData<T>> QueryPageAsync(GetT? input, PageRequest? page = null);
    /// <summary>
    /// 列表查询
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(GetT? input);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids);
}