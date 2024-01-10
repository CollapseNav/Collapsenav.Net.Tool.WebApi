using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class QueryRepApplication<T, GetT> : Application<T>, IQueryApplication<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected new readonly IQueryRepository<T> Repo;
    protected readonly IMap Mapper;
    public QueryRepApplication(IQueryRepository<T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        Mapper = mapper;
    }
    public virtual IQueryable<T> GetQuery(GetT? input)
    {
        return input?.GetQuery(Repo) ?? Repo.Query();
    }
    public virtual IQueryable<T> GetQuery<NewGetT>(NewGetT? input) where NewGetT : IBaseGet<T>
    {
        return input?.GetQuery(Repo) ?? Repo.Query();
    }
    public virtual async Task<PageData<T>> QueryPageAsync(GetT? input, PageRequest? page = null)
    {
        return await Repo.QueryPageAsync(input?.GetQuery(Repo), page);
    }
    public virtual async Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IBaseGet<T, ReturnT>? input, PageRequest? page = null)
    {
        return await Repo.QueryPageAsync(input?.GetQuery(Repo), page);
    }
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT? input) => await Repo.QueryAsync(GetQuery(input));
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT? input) where NewGetT : class, IBaseGet<T> => await Repo.QueryAsync(GetQuery(input));
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT? input)
    {
        return Mapper.Map<IEnumerable<ReturnT>>(await Repo.QueryAsync(GetQuery(input))) ?? Enumerable.Empty<ReturnT>();
    }
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(IBaseGet<T, ReturnT>? input)
    {
        return await Task.Factory.StartNew(() =>
        {
            return input?.GetQuery(Repo) ?? Enumerable.Empty<ReturnT>();
        });
    }
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp) => await Repo.IsExistAsync(exp);
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? exp = null) => await Repo.CountAsync(exp);
    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id) => await Repo.GetByIdAsync(id);
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids) => await Repo.QueryByIdsAsync(ids);
}
