using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ISimpleApplication<T> : INoConstraintsSimpleApplication<T>, IApplication<T>
    where T : class, IEntity
{
}