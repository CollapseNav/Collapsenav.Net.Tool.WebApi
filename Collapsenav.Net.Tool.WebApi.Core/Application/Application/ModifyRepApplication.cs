using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public class ModifyRepApplication<T, CreateT> : WriteRepApplication<T>, IModifyApplication<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected new readonly IModifyRepository<T> Repo;
    protected readonly IMap Mapper;
    public ModifyRepApplication(IModifyRepository<T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        Mapper = mapper;
    }
    public virtual async Task<T?> AddAsync(CreateT? entity)
    {
        if (entity == null)
            throw new Exception();
        if (entity.IsExist(Repo.Query()))
            throw new Exception("Data Exist");
        var data = Mapper.Map<T>(entity);
        if (data == null)
            return null;
        return await Repo.AddAsync(data);
    }
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys)
    {
        if (entitys == null)
            throw new Exception();
        if (entitys.Any(item => item.IsExist(Repo.Query())))
            throw new Exception("Data Exist");
        var datas = entitys.Select(item => Mapper.Map<T>(item));
        if (datas == null)
            return 0;
        return await Repo.AddAsync(datas);
    }

    public virtual async Task<int> UpdateAsync(IBaseUpdate<T> entity)
    {
        var where = entity.GetWhereExpression();
        var update = entity.GetUpdateExpression();
        return await Repo.UpdateAsync(where, update);
    }

    public virtual async Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false) => await Repo.DeleteByIdsAsync(id, isTrue);
    public virtual async Task<int> UpdateAsync<TKey>(TKey? id, CreateT? entity)
    {
        if (entity == null)
            throw new Exception();
        if (entity.IsExist(Repo.Query()))
            throw new Exception("Data Exist");
        var data = Mapper.Map<T>(entity);
        if (data == null)
            return 0;
        data.SetKeyValue(id);
        return await Repo.UpdateAsync(data);
    }
}