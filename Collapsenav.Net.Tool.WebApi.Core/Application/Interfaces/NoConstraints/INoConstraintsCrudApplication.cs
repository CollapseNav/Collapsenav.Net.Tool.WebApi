namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsCrudApplication<T, CreateT, GetT> : INoConstraintsQueryApplication<T, GetT>, INoConstraintsModifyApplication<T, CreateT>
{
}
public interface INoConstraintsCrudApplication<TKey, T, CreateT, GetT> : INoConstraintsCrudApplication<T, CreateT, GetT>, INoConstraintsQueryApplication<TKey, T, GetT>, INoConstraintsModifyApplication<TKey, T, CreateT>
{
}