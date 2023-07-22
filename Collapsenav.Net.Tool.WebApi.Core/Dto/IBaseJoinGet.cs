using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface IBaseJoinGet<T, ReturnDto> : IBaseGet<T, ReturnDto> where T : class, IEntity
{
    new IQueryable<ReturnDto> GetQuery(IRepository<T> repository);
}