using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Http;

namespace Collapsenav.Net.Tool.WebApi;

public class AutoTransactionErrorHandler
{
    public static Task Run(HttpContext context)
    {
        TransManager.HasError = true;
        return Task.CompletedTask;
    }
}