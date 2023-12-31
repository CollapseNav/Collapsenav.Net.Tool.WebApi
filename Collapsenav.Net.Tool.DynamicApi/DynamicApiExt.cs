using Collapsenav.Net.Tool.Data;
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
        services.AddControllers().AddDynamicWebApi(config ?? new DynamicApiConfig().UseDefault());
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder, DynamicApiConfig? config = null)
    {
        DynamicApiConfig.GlobalConfig = config ?? new();
        var dynamicApiTypes = AppDomain.CurrentDomain.GetTypes().Where(item => !item.IsInterface && !item.IsAbstract && (item.HasAttribute<DynamicApiAttribute>() || item.IsType<IDynamicApi>()));
        if (dynamicApiTypes.NotEmpty())
            dynamicApiTypes.ForEach(item => builder.Services.AddScoped(item));
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

    internal static DynamicController AddController<T>(this IServiceCollection services, string route)
    {
        var controller = new DynamicController(typeof(T), route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    public static DynamicController AddQueryController<Entity, GetDto>(this IServiceCollection services, string route)
        where Entity : class, IEntity
        where GetDto : IBaseGet<Entity>
    {
        return services.AddController<QueryAppController<Entity, GetDto>>(route);
    }

    public static DynamicController AddCrudController<Entity, CreateDto, GetDto>(this IServiceCollection services, string route)
        where Entity : class, IEntity
        where GetDto : IBaseGet<Entity>
        where CreateDto : IBaseCreate<Entity>
    {
        return services.AddController<CrudAppController<Entity, CreateDto, GetDto>>(route);
    }
}