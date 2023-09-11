using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
[ApiController]
[Route("[controller]")]
public class ModifyRepController<T, CreateT> : ControllerBase, IModifyController<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected readonly IModifyRepository<T> Repository;
    protected readonly IMap Mapper;
    public ModifyRepController(IModifyRepository<T> repository, IMap mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost, Route("")]
    public virtual async Task<T?> AddAsync([FromBody] CreateT? entity)
    {
        if (entity == null)
            return null;
        if (entity.IsExist(Repository.Query()))
            throw new Exception("Data Exist");
        var data = Mapper.Map<T>(entity);
        if (data == null)
            return null;
        return await Repository.AddAsync(data);
    }
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT>? entitys)
    {
        if (entitys == null)
            return 0;
        if (entitys.Any(item => item.IsExist(Repository.Query())))
            throw new Exception("Data Exist");
        var datas = entitys.Select(item => Mapper.Map<T>(item));
        if (datas == null)
            return 0;
        return await Repository.AddAsync(datas);
    }
    [NonAction]
    public void Dispose() => Repository.Save();
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string? id, [FromQuery] bool isTrue = false) => await Repository.DeleteAsync(id, isTrue);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete, Route("")]
    public virtual async Task<int> DeleteRangeAsync([FromQuery] IEnumerable<string>? id, [FromQuery] bool isTrue = false) => await Repository.DeleteAsync(id, isTrue);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(string? id, CreateT? entity)
    {
        if (entity.IsExist(Repository.Query()))
            throw new Exception("Data Exist");
        var data = Mapper.Map<T>(entity);
        if (data == null)
            return;
        data.SetValue("Id", id);
        await Repository.UpdateAsync(data);
    }
}