using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个只能新增和删除的 controller
/// </summary>
public class SecondController : CrudAppController<SecondEntity, SecondCreateDto, SecondGetDto>
{
    public SecondController(ICrudApplication<SecondEntity, SecondCreateDto, SecondGetDto> app, IMap mapper) : base(app, mapper)
    {
    }
}
