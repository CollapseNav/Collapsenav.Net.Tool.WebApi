using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyApplication<T, CreateT> : INoConstraintsModifyApplication<T, CreateT>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}