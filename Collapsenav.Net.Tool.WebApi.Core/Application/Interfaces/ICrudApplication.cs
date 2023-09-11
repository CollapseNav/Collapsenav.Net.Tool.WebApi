using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;
public interface ICrudApplication<T, CreateT, GetT> : INoConstraintsCrudApplication<T, CreateT, GetT>, IQueryApplication<T, GetT>, IModifyApplication<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
}