using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;

public interface IBaseUpdate { }
public interface IBaseUpdate<T> : IBaseUpdate
{
    Expression<Func<T, T>> GetUpdateExpression();
    Expression<Func<T, bool>> GetWhereExpression();
}