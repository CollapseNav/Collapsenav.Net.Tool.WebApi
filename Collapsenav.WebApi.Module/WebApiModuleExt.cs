using Collapsenav.Module;
using Microsoft.AspNetCore.Builder;

namespace Collapsenav.WebApi.Module;

public static class WebApiModuleExt
{
    public static WebApplicationBuilder LoadModules(this WebApplicationBuilder? builder)
    {
        if (builder is null)
            throw new Exception();
        builder.Services.LoadModules(builder.Host, builder.Configuration, builder.Environment);
        return builder;
    }
}