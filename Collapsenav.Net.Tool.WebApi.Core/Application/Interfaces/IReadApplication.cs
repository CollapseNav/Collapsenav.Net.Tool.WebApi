using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadApplication<T> : INoConstraintsReadApplication<T>
    where T : IEntity
{
}
public interface IReadApplication<TKey, T> : INoConstraintsReadApplication<TKey, T>, IReadApplication<T>
    where T : IEntity<TKey>
{
}