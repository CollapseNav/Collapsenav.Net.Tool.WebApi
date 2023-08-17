using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ICheckExistApplication<T> : INoConstraintsCheckExistApplication<T>, IApplication<T> where T : IEntity
{
}

#region 无泛型约束
public interface INoConstraintsCheckExistApplication<T> : IApplication<T>
{
    /// <summary>
    /// 是否存在
    /// </summary>
    Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp);
}
#endregion
