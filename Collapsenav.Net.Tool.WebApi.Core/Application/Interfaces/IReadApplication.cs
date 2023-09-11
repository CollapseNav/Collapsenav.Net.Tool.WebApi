using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadApplication<T> : INoConstraintsReadApplication<T>
    where T : IEntity
{
}