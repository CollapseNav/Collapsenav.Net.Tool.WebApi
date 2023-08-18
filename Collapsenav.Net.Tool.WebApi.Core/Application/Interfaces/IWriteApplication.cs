using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteApplication<T> : INoConstraintsWriteApplication<T>, IApplication<T>, IDisposable
    where T : IEntity
{
}
public interface IWriteApplication<TKey, T> : INoConstraintsWriteApplication<TKey, T>, IWriteApplication<T>
    where T : IEntity<TKey>
{
}