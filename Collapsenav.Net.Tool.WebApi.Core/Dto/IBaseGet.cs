using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseGet
{
    IQueryable GetQuery(IQueryable query);
}
public interface IBaseGet<T> : IBaseGet
{
    IQueryable<T> GetQuery(IQueryable<T> query);
}
