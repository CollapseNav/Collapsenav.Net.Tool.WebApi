using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadController<T> : INoConstraintsReadController<T>, IController
    where T : class, IEntity
{
}