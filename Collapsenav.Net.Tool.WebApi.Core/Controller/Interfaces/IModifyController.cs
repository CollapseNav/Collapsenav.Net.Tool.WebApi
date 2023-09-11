using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IModifyController<T, CreateT> : INoConstraintsModifyController<T, CreateT>, IWriteController<T, CreateT>
, IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}