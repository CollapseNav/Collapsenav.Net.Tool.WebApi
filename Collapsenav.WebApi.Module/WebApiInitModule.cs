using Collapsenav.Module;
using Collapsenav.Net.Tool.AutoInject;
using Collapsenav.Net.Tool.WebApi;
using Collapsenav.Net.Tool.DynamicApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Collapsenav.WebApi.Module;

public class WebApiInitModule : InitModule
{
    public virtual void Init(IServiceCollection services, IHostBuilder? hostBuilder = null, IConfiguration? configuration = null, IHostEnvironment? environment = null)
    {
        Init(hostBuilder, configuration);
        Init(services, configuration);
    }
    private void Init(IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddControllers().AddControllersAsServices();
        services.AddDynamicWebApi().AddRepController().AddAppController();
        services.AddAutoController();
        services.AddDefaultSwaggerGen();
        services.AddAutoInjectController();
    }
    private void Init(IHostBuilder? hostBuilder, IConfiguration? configuration = null)
    {
        if (hostBuilder is null)
            return;
        hostBuilder.UseAutoInjectProviderFactory();
    }

    public virtual void Use(IApplicationBuilder app, IConfiguration? configuration = null, IHostEnvironment? environment = null)
    {
        app.UseSwagger().UseSwaggerUI();
    }
}
