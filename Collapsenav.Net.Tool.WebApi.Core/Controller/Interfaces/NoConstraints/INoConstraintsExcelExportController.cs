using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;


public interface INoConstraintsExcelExportController<T, GetT> : INoConstraintsController
{
    Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT? input);
}
