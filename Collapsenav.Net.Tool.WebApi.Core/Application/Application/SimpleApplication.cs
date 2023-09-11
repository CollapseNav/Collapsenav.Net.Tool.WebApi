using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class SimpleApplication<T> : Application<T>, ISimpleApplication<T>
    where T : class, IEntity
{
    protected new readonly ICrudRepository<T> Repo;
    public SimpleApplication(ICrudRepository<T> repository) : base(repository)
    {
        Repo = repository;
    }

    public virtual async Task DeleteAsync(string? id, bool isTrue = false)
    {
        if (id == null)
            return;
        await Repo.DeleteAsync(id, isTrue);
    }

    public virtual async Task<T?> AddAsync(T? entity)
    {
        return await Repo.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T? entity)
    {
        await Repo.UpdateAsync(entity);
    }

    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id) => await Repo.GetByIdAsync(id);
    public virtual async Task<T?> GetByIdAsync(string? id)
    {
        return await Repo.GetByIdAsync(id);
    }
}