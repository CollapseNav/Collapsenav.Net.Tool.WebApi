using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;

public abstract class BaseUpdate : IBaseUpdate
{
}

public abstract class BaseUpdate<T> : BaseUpdate, IBaseUpdate<T>
{
    public abstract Expression<Func<T, T>> GetUpdateExpression();

    public abstract Expression<Func<T, bool>> GetWhereExpression();
}


