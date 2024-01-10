using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyController<T, CreateT> : INoConstraintsModifyController<T, CreateT>
, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}