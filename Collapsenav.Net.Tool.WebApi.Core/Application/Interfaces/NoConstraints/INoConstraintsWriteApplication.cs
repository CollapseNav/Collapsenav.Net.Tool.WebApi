namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsWriteApplication<T> : IApplication<T>, IDisposable
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(string? id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T?> AddAsync(T? entity);
    /// <summary>
    /// 修改
    /// </summary>
    Task<int> UpdateAsync(string? id, T? entity);
    void Save();
    Task SaveAsync();
}
public interface INoConstraintsWriteApplication<TKey, T> : INoConstraintsWriteApplication<T>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(TKey? id, bool isTrue = false);
}