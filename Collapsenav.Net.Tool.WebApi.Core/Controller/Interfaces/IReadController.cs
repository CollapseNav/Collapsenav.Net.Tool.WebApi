using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadController<T> : INoConstraintsReadController<T>, IController
    where T : class, IEntity
{
}
public interface IReadController<TKey, T> : INoConstraintsReadController<TKey, T>, IReadController<T>
    where T : class, IEntity<TKey>
{
}
