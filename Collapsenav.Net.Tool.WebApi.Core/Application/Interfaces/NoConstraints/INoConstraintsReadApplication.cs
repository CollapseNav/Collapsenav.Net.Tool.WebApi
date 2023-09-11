namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsReadApplication<T> : IApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T?> GetByIdAsync<TKey>(TKey? id);
}