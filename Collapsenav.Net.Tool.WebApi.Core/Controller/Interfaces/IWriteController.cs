using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteController<T, CreateT> : INoConstraintsWriteController<T, CreateT>, IController
// , IDisposable
    where T : IEntity
    where CreateT : IBaseCreate
{
}
