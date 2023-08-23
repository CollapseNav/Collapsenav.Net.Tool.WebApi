namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsSimpleApplication<T> : INoConstraintsReadApplication<T>, IApplication<T>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task DeleteAsync(string? id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T?> AddAsync(T? entity);
    /// <summary>
    /// 修改
    /// </summary>
    Task UpdateAsync(T? entity);
}