using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;
using Microsoft.AspNetCore.Mvc;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个可以增删改查的 controller
/// </summary>
public class FirstCrudController : CrudAppController<FirstEntity, FirstCreateDto, FirstGetDto>, IExcelExportController<FirstEntity, FirstGetDto2>
{
    public FirstCrudController(ICrudApplication<FirstEntity, FirstCreateDto, FirstGetDto> app, IMap mapper) : base(app, mapper)
    {
    }

    public async Task<FileStreamResult> ExportExcelAsync([FromQuery] FirstGetDto2 input)
    {
        var data = await App.QueryAsync<FirstGetDto2, WeatherForecast>(input);
        return new FileStreamResult(new MemoryStream(), "");
    }
}
