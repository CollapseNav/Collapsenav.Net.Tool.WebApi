using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsModifyController<T, CreateT> : INoConstraintsWriteController<T, CreateT>, IDisposable
{
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys);
}
public interface INoConstraintsModifyController<TKey, T, CreateT> : INoConstraintsWriteController<TKey, T, CreateT>, INoConstraintsModifyController<T, CreateT>
{
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete("ByIds")]
    Task<int> DeleteRangeAsync([FromQuery] IEnumerable<TKey>? id, [FromQuery] bool isTrue = false);
}
