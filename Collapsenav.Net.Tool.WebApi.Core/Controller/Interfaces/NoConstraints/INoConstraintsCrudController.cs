namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsCrudController<T, CreateT, GetT> : INoConstraintsQueryController<T, GetT>, INoConstraintsModifyController<T, CreateT>
{
}
public interface INoConstraintsCrudController<TKey, T, CreateT, GetT> : INoConstraintsQueryController<TKey, T, GetT>, INoConstraintsModifyController<TKey, T, CreateT>
{
}