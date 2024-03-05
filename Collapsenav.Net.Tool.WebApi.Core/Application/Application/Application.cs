using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class Application<T> : IApplication<T> where T : class, IEntity
{
    protected IRepository<T> Repo;
    public Application(IRepository<T> repository)
    {
        Repo = repository;
    }
}
