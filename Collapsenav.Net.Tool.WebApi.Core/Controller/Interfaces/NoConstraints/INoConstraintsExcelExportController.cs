using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;


public interface INoConstraintsExcelExportController<T, GetT> : IController
{
    Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT? input);
}
