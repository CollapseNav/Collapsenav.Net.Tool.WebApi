using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyApplication<T, CreateT> : INoConstraintsModifyApplication<T, CreateT>, IWriteApplication<T>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}
public interface IModifyApplication<TKey, T, CreateT> : INoConstraintsModifyApplication<TKey, T, CreateT>, IModifyApplication<T, CreateT>, IWriteApplication<TKey, T>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
}

#region 无泛型约束
public interface INoConstraintsModifyApplication<T, CreateT> : INoConstraintsWriteApplication<T>, IDisposable
{
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T?> AddAsync(CreateT? entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys);
    Task<int> UpdateAsync(IBaseUpdate<T> entity);
}
public interface INoConstraintsModifyApplication<TKey, T, CreateT> : INoConstraintsModifyApplication<T, CreateT>, INoConstraintsWriteApplication<TKey, T>
{
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    Task<int> DeleteRangeAsync(IEnumerable<TKey>? id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task<int> UpdateAsync(TKey? id, CreateT? entity);
}
#endregion


