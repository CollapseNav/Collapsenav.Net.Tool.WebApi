using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsCountApplication<T> : IApplication<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? exp = null);
}