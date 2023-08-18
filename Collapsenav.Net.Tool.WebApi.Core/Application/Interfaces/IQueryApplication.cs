using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryApplication<T, GetT> : INoConstraintsQueryApplication<T, GetT>, IReadApplication<T>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    IQueryable<T> GetQuery<NewGetT>(NewGetT? input) where NewGetT : IBaseGet<T>;
    Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IBaseGet<T, ReturnT>? input, PageRequest? page = null);
    Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT? input);
    Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(IBaseGet<T, ReturnT>? input);
    Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT? input) where NewGetT : class, IBaseGet<T>;
}
public interface IQueryApplication<TKey, T, GetT> : INoConstraintsQueryApplication<TKey, T, GetT>, IQueryApplication<T, GetT>,
IReadApplication<TKey, T>
    where T : class, IEntity<TKey>
    where GetT : IBaseGet<T>
{
}
