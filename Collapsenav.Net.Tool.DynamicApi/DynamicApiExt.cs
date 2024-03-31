using System.Reflection;
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
    internal static DynamicController AddController(this IServiceCollection services, Type controllerType, string route)
    {
        var controller = new DynamicController(controllerType, route);
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

    public static DynamicController AddQueryController(this IServiceCollection services, Type entityType, Type getDtoType, string route)
    {
        var controller = new DynamicController(entityType, getDtoType, route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    public static DynamicController AddCrudController(this IServiceCollection services, Type entityType, Type createDtoType, Type getDtoType, string route)
    {
        var controller = new DynamicController(entityType, createDtoType, getDtoType, route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }

    public static IServiceCollection AddAutoController(this IServiceCollection services)
    {
        // 先查出所有的继承IBaseGet和IBaseCreate的类型
        var types = AppDomain.CurrentDomain.GetTypes(typeof(IBaseGet), typeof(IBaseCreate));
        // 然后根据MapControllerAttribute的Value分组（就是controller的路由）
        var controllerGroups = types.Where(i => i.HasAttribute<MapControllerAttribute>()).GroupBy(i => i.GetCustomAttribute<MapControllerAttribute>()!.Value).ToList();
        // 然后创建对应的controller
        foreach (var controllerGroup in controllerGroups)
        {
            Type? entityType = null;
            Type? getType = null;
            Type? createType = null;
            // 找到继承IBaseGet和IBaseCreate的类型
            if (controllerGroup.Any(i => i.IsType(typeof(IBaseGet))))
                getType = controllerGroup.First(i => i.IsType(typeof(IBaseGet)));
            if (controllerGroup.Any(i => i.IsType(typeof(IBaseCreate))))
                createType = controllerGroup.First(i => i.IsType(typeof(IBaseCreate)));
            // 首先是必须要有个get
            if (getType == null)
                continue;
            // 如果没有create，则认为是查询controller
            else if (createType == null)
            {
                entityType = getType.BaseType!.GenericTypeArguments.First(i => i.IsType(typeof(IEntity)));
                services.AddQueryController(entityType, getType, controllerGroup.Key);
            }
            // 如果有create，则认为是增删改查controller
            else if (createType != null)
            {
                entityType = getType.BaseType!.GenericTypeArguments.First(i => i.IsType(typeof(IEntity)));
                var createEntityType = createType.BaseType!.GenericTypeArguments.First(i => i.IsType(typeof(IEntity)));
                // 此时需要保证get和create的实体类型一致
                if (entityType == createEntityType)
                    services.AddCrudController(entityType, createType, getType, controllerGroup.Key);
                else
                    continue;
            }
        }
        return services;
    }
}