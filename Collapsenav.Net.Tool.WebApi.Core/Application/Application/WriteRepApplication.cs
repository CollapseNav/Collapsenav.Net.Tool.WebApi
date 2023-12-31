using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public class WriteRepApplication<T> : Application<T>, IWriteApplication<T>
    where T : class, IEntity
{
    protected new readonly IWriteRepository<T> Repo;
    public WriteRepApplication(IWriteRepository<T> repository) : base(repository)
    {
        Repo = repository;
    }
    public virtual async Task<T?> AddAsync(T? entity) => await Repo.AddAsync(entity);

    public virtual async Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false) => await Repo.DeleteAsync(id, isTrue);
    public void Dispose() => Repo.Save();

    public virtual void Save() => Repo.Save();

    public virtual async Task SaveAsync() => await Repo.SaveAsync();

    public virtual async Task<int> UpdateAsync(string? id, T? entity)
    {
        var tid = id?.ToValue(Repo.KeyType() ?? throw new Exception(""));
        var prop = Repo.KeyProp() ?? throw new Exception("");
        entity.SetValue(prop.Name, tid);
        return await Repo.UpdateAsync(entity);
    }
}

