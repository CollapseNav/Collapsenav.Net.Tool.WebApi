using Collapsenav.Net.Tool.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.DynamicApi;

public static class DynamicApiExt
{
    internal static bool HasControllerRoute { get; set; } = false;
    /// <summary>
    /// 判断type是否被标记为 动态api
    /// </summary>
    public static bool IsDynamicApi(this Type type)
    {
        if ((type.HasAttribute<DynamicApiAttribute>() || type.HasInterface(typeof(IDynamicApi))) && !type.IsInterface && !type.IsAbstract)
            return true;
        return false;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, ApiJsonConfig config)
    {
        services.AddControllers().AddDynamicWebApi(config.BuildApiConfig());
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, DynamicApiConfig? config = null)
    {
        services.AddControllers().AddDynamicWebApi(config ?? new DynamicApiConfig().AddDefault());
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder, DynamicApiConfig? config = null)
    {
        DynamicApiConfig.GlobalConfig = config ?? new();
        // 添加自定义的 DynamicApiProvider 用以识别标记的api
        builder.ConfigureApplicationPartManager(part =>
        {
            part.FeatureProviders.Add(new DynamicApiProvider());
        });
        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new ApplicationServiceConvention());
        });
        return builder;
    }

    public static IServiceCollection AddController<T>(this IServiceCollection services, string route)
    {
        ApplicationServiceConvention.DynamicControllers.Add(new DynamicController(typeof(T), route));
        return services;
    }
}