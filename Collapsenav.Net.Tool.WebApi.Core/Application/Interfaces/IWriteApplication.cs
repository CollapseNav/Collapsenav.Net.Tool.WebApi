using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteApplication<T> : INoConstraintsWriteApplication<T>, IApplication<T>, IDisposable
    where T : IEntity
{
}