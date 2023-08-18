namespace Collapsenav.Net.Tool.WebApi;

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