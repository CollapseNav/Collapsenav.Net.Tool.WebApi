using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyController<T, CreateT> : INoConstraintsModifyController<T, CreateT>, IWriteController<T, CreateT>, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}
public interface IModifyController<TKey, T, CreateT> : INoConstraintsModifyController<TKey, T, CreateT>, IWriteController<TKey, T, CreateT>, IModifyController<T, CreateT>
    where T : IEntity<TKey>
    where CreateT : IBaseCreate
{
}