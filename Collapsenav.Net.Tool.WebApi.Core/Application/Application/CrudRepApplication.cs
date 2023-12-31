using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public class CrudRepApplication<T, CreateT, GetT> : ICrudApplication<T, CreateT, GetT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
    protected ICrudRepository<T> Repo;
    protected IModifyApplication<T, CreateT> Write;
    protected IQueryApplication<T, GetT> Read;
    public CrudRepApplication(ICrudRepository<T> repo, IMap mapper)
    {
        Repo = repo;
        Write = new ModifyRepApplication<T, CreateT>(Repo, mapper);
        Read = new QueryRepApplication<T, GetT>(Repo, mapper);
    }
    public virtual void Save() => Repo.Save();
    public virtual async Task SaveAsync() => await Repo.SaveAsync();
    public virtual async Task<T?> AddAsync(CreateT? entity) => await Write.AddAsync(entity);
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys) => await Write.AddRangeAsync(entitys);
    public virtual async Task<PageData<T>> QueryPageAsync(GetT? input, PageRequest? page = null) => await Read.QueryPageAsync(input, page);
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT? input) => await Read.QueryAsync(input);
    public virtual void Dispose() => Repo.Save();
    public virtual IQueryable<T> GetQuery(GetT? input) => Read.GetQuery(input);
    public virtual async Task<T?> GetByIdAsync(string? id) => await Read.GetByIdAsync(id);
    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id) => await Repo.GetByIdAsync(id);
    public virtual async Task<bool> DeleteAsync(string? id, bool isTrue = false) => await Write.DeleteAsync(id, isTrue);
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT? input) where NewGetT : class, IBaseGet<T> => await Read.QueryAsync(input);
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT? input) => await Read.QueryAsync<ReturnT>(input);
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(IBaseGet<T, ReturnT>? input) => await Read.QueryAsync(input);
    public virtual async Task<T?> AddAsync(T? entity) => await Write.AddAsync(entity);
    public virtual async Task<int> UpdateAsync(string? id, T? entity) => await Write.UpdateAsync(id, entity);
    public virtual IQueryable<T> GetQuery<NewGetT>(NewGetT? input) where NewGetT : IBaseGet<T> => Read.GetQuery(input);
    public virtual async Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IBaseGet<T, ReturnT>? input, PageRequest? page = null) => await Read.QueryPageAsync(input, page);
    public virtual async Task<int> UpdateAsync(IBaseUpdate<T> entity) => await Write.UpdateAsync(entity);
    public virtual async Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false) => await Write.DeleteAsync(id, isTrue);
    public virtual async Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false) => await Write.DeleteRangeAsync(id, isTrue);
    public virtual async Task<int> UpdateAsync<TKey>(TKey? id, CreateT? entity) => await Write.UpdateAsync(id, entity);
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids) => await Read.QueryByIdsAsync(ids);
}