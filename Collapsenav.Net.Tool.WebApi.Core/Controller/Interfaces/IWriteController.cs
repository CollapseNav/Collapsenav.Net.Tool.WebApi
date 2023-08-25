using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteController<T, CreateT> : INoConstraintsWriteController<T, CreateT>, IController, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}
public interface IWriteController<TKey, T, CreateT> : INoConstraintsWriteController<TKey, T, CreateT>, IWriteController<T, CreateT>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
}
