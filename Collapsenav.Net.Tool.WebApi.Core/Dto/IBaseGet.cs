using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseGet
{
    // IQueryable? GetQuery(IQueryable? query);
}
public interface IBaseGet<T> : IBaseGet<T, T> where T : IEntity { }

public interface IBaseGet<T, ReturnDto> : IBaseGet where T : IEntity
{
    IQueryable<ReturnDto> GetQuery(IRepository<T> repo);
}