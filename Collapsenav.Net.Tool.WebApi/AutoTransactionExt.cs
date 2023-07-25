using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Builder;

namespace Collapsenav.Net.Tool.WebApi;

public static class TransactionActionExt
{
    public static IApplicationBuilder UseAutoCommit(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(AutoTransactionErrorHandler.Run);
        });
        TransManager.UseAutoCommit();
        return app;
    }
}