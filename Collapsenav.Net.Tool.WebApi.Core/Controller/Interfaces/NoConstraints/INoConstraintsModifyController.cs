using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;

public interface INoConstraintsModifyController<T, CreateT> : INoConstraintsWriteController<T, CreateT>
// , IDisposable
{
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete, Route("ByIds")]
    Task<int> DeleteRangeAsync([FromQuery] IEnumerable<string>? id, [FromQuery] bool isTrue = false);
}
