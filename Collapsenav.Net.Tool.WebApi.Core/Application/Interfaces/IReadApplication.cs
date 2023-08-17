using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadApplication<T> : INoConstraintsReadApplication<T>
    where T : IEntity
{
}
public interface IReadApplication<TKey, T> : INoConstraintsReadApplication<TKey, T>, IReadApplication<T>
    where T : IEntity<TKey>
{
}

#region 无泛型约束
public interface INoConstraintsReadApplication<T> : IApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T?> QueryByStringIdAsync(string? id);
}
public interface INoConstraintsReadApplication<TKey, T> : INoConstraintsReadApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T?> QueryAsync(TKey? id);
}
#endregion
