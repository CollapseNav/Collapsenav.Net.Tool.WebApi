using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyApplication<T, CreateT> : INoConstraintsModifyApplication<T, CreateT>, IWriteApplication<T>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}
public interface IModifyApplication<TKey, T, CreateT> : INoConstraintsModifyApplication<TKey, T, CreateT>, IModifyApplication<T, CreateT>, IWriteApplication<TKey, T>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
}

