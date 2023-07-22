using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public abstract class BaseGet : IBaseGet { }
public abstract class BaseGet<T> : BaseGet<T, T>, IBaseGet<T> where T : class, IEntity { }
public abstract class BaseGet<T, ReturnDto> : BaseGet, IBaseGet<T, ReturnDto> where T : class, IEntity
{
    public abstract IQueryable<ReturnDto> GetQuery(IRepository<T> repo);
}