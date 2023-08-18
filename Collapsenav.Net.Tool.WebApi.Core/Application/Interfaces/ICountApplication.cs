using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;
public interface ICountApplication<T> : INoConstraintsCountApplication<T> where T : IEntity
{
}
