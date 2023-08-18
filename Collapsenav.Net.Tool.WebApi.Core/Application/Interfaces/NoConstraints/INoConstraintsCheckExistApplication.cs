using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public interface INoConstraintsCheckExistApplication<T> : IApplication<T>
{
    /// <summary>
    /// 是否存在
    /// </summary>
    Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp);
}