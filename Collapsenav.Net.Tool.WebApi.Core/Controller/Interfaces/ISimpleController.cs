using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public interface ISimpleController<T> : INoConstraintsSimpleController<T>, IController
    where T : IEntity
{
}