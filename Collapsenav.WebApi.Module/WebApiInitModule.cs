﻿using Collapsenav.Module;
using Collapsenav.Net.Tool.AutoInject;
using Collapsenav.Net.Tool.WebApi;
using Collapsenav.Net.Tool.DynamicApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Collapsenav.WebApi.Module;

public class WebApiInitModule : InitModule
{
    public override void Init(IServiceCollection services, IHostBuilder? hostBuilder = null, IConfiguration? configuration = null, IHostEnvironment? environment = null)
    {
        Init(hostBuilder, configuration);
        Init(services, configuration);
    }
    private void Init(IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddControllers().AddControllersAsServices();
        services.AddDynamicWebApi().AddRepController().AddAppController();
        services.AddDefaultSwaggerGen();
        services.AddAutoInjectController();
    }
    private void Init(IHostBuilder? hostBuilder, IConfiguration? configuration = null)
    {
        if (hostBuilder is null)
            return;
        hostBuilder.UseAutoInjectProviderFactory();
    }
}
