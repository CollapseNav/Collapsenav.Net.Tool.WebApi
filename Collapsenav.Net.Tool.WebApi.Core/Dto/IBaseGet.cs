using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseGet
{
    // IQueryable? GetQuery(IQueryable? query);
}
public interface IBaseGet<T> : IBaseGet<T, T> where T : class, IEntity { }

public interface IBaseGet<T, ReturnDto> : IBaseGet where T : class, IEntity
{
    IQueryable<ReturnDto> GetQuery(IRepository<T> repo);
}