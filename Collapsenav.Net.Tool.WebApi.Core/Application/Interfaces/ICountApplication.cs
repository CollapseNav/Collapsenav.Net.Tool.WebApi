using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICountApplication<T> : INoConstraintsCountApplication<T> where T : IEntity
{
}

#region 无泛型约束
public interface INoConstraintsCountApplication<T> : IApplication<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? exp = null);
}
#endregion
