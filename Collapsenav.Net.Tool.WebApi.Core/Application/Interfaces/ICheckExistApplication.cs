using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;
public interface ICheckExistApplication<T> : INoConstraintsCheckExistApplication<T>, IApplication<T> where T : IEntity
{
}
