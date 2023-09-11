namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsCrudController<T, CreateT, GetT> : INoConstraintsQueryController<T, GetT>, INoConstraintsModifyController<T, CreateT>
{
}