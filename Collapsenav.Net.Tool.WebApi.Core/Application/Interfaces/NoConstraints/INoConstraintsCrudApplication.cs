namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsCrudApplication<T, CreateT, GetT> : INoConstraintsQueryApplication<T, GetT>, INoConstraintsModifyApplication<T, CreateT>
{
}