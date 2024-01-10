namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsModifyApplication<T, CreateT> : IApplication<T>, IDisposable
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false);
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
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T?> AddAsync(CreateT? entity);
    /// <summary>
    /// 添加(多个)
    /// </summary>
    Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys);
    Task<int> UpdateAsync(IBaseUpdate<T> entity);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    Task<int> UpdateAsync<TKey>(TKey? id, CreateT? entity);
}