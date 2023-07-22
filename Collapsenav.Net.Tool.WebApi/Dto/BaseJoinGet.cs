using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public abstract class BaseJoinGet<T, ReturnDto> : BaseGet<T, ReturnDto>, IBaseJoinGet<T, ReturnDto>
where T : class, IEntity
{
}