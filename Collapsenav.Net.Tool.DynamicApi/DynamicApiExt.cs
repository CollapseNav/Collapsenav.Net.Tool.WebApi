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
    /// <summary>
    /// 注册controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    internal static DynamicController AddController<T>(this IServiceCollection services, string route)
    {
        var controller = new DynamicController(typeof(T), route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    /// <summary>
    /// 注册controller
    /// </summary>
    /// <param name="services"></param>
    /// <param name="controllerType"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    internal static DynamicController AddController(this IServiceCollection services, Type controllerType, string route)
    {
        var controller = new DynamicController(controllerType, route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    /// <summary>
    /// 注册用于查询的controller
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="GetDto"></typeparam>
    /// <param name="services"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    public static DynamicController AddQueryController<Entity, GetDto>(this IServiceCollection services, string route)
        where Entity : class, IEntity
        where GetDto : IBaseGet<Entity>
    {
        return services.AddController<QueryAppController<Entity, GetDto>>(route);
    }
    /// <summary>
    /// 注册Crud controller
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// <typeparam name="CreateDto"></typeparam>
    /// <typeparam name="GetDto"></typeparam>
    /// <param name="services"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    public static DynamicController AddCrudController<Entity, CreateDto, GetDto>(this IServiceCollection services, string route)
        where Entity : class, IEntity
        where GetDto : IBaseGet<Entity>
        where CreateDto : IBaseCreate<Entity>
    {
        return services.AddController<CrudAppController<Entity, CreateDto, GetDto>>(route);
    }
    /// <summary>
    ///  注册用于查询的controller
    /// </summary>
    /// <param name="services"></param>
    /// <param name="entityType"></param>
    /// <param name="getDtoType"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    public static DynamicController AddQueryController(this IServiceCollection services, Type entityType, Type getDtoType, string route)
    {
        var controller = new DynamicController(entityType, getDtoType, route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    /// <summary>
    /// 注册Crud controller
    /// </summary>
    /// <param name="services"></param>
    /// <param name="entityType"></param>
    /// <param name="createDtoType"></param>
    /// <param name="getDtoType"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    public static DynamicController AddCrudController(this IServiceCollection services, Type entityType, Type createDtoType, Type getDtoType, string route)
    {
        var controller = new DynamicController(entityType, createDtoType, getDtoType, route);
        ApplicationServiceConvention.DynamicControllers.Add(controller);
        return controller;
    }
    /// <summary>
    /// 自动根据注解添加controller
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoController(this IServiceCollection services)
    {
        // 先查出所有的继承IBaseGet和IBaseCreate的类型, 作为泛型参数
        var markedTypes = AppDomain.CurrentDomain.GetTypes(typeof(IBaseGet), typeof(IBaseCreate));
        // 然后根据MapControllerAttribute的Value分组（就是controller的路由）
        var controllerGroups = markedTypes.Where(i => i.CustomAttributes.Any(t => t.AttributeType.IsType<MapControllerAttribute>()))
                            .GroupBy(i => i.GetCustomAttribute<MapControllerAttribute>(true)!.Value).ToList();
        // 然后创建对应的controller
        foreach (var (route, types) in controllerGroups.Select(i => (i.Key, i.ToList())))
        {
            if (route.IsEmpty() || types.IsEmpty())
                continue;
            var createTypes = types.Where(i => i.IsType<IBaseCreate>()).ToList();
            var getTypes = types.Where(i => i.IsType<IBaseGet>()).ToList();
            Type? getType = null;
            Type? createType = null;
            List<Type> getPageTypes = new();
            DynamicController controller;

            // 暂时假定每个route下都只有一个create
            if (createTypes.Any())
                createType = createTypes.First();

            // 找到继承IBaseGet和IBaseJoin的类型
            if (getTypes.Any())
            {
                if (getTypes.Count == 1 && !getTypes.First().IsType(typeof(IBaseJoinGet<,>)))
                {
                    getType = getTypes.First();
                    getTypes = getTypes.Where(i => i != getType).ToList();
                }

                foreach (var cg in getTypes)
                    if (cg.GetCustomAttribute<MapControllerAttribute>()!.IsPage)
                        getPageTypes.Add(cg);
                getTypes = getTypes.Except(getPageTypes).ToList();

                if (getTypes.NotEmpty())
                    getType = getTypes.First(i => i.IsType(typeof(IBaseGet)) && !i.IsType(typeof(IBaseJoinGet<,>)));

                if (getPageTypes.NotEmpty())
                    getType = getPageTypes.First(i => i.IsType(typeof(IBaseGet)) && !i.IsType(typeof(IBaseJoinGet<,>)));
            }

            // 首先是必须要有个get
            if (getType == null)
                continue;
            else
                controller = GetController(services, getType, createType, route);
            AddGetAction(controller, getTypes);
            AddGetPageAction(controller, getPageTypes);
        }
        return services;
    }

    /// <summary>
    /// 创建基础的controller
    /// </summary>
    /// <param name="services"></param>
    /// <param name="getType"></param>
    /// <param name="createType"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    private static DynamicController GetController(IServiceCollection services, Type getType, Type? createType, string route)
    {
        var entityType = getType.BaseType!.GenericTypeArguments.First(i => i.IsType(typeof(IEntity)));
        if (createType == null)
            return services.AddQueryController(entityType, getType, route);
        else
        {
            var createEntityType = createType.BaseType!.GenericTypeArguments.First(i => i.IsType(typeof(IEntity)));
            // 此时需要保证get和create的实体类型一致
            return services.AddCrudController(entityType, createType, getType, route);
        }
    }
    /// <summary>
    /// 添加普通的列表接口
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="types"></param>
    /// <param name="attributes"></param>
    private static void AddGetPageAction(DynamicController controller, IEnumerable<Type> types, List<Attribute>? attributes = null)
    {
        if (types.IsEmpty())
            return;
        attributes = new List<Attribute>();
        foreach (var type in types)
        {
            var actionRoute = type.GetCustomAttribute<MapControllerAttribute>()!.ActionName;
            var attr = type.GetCustomAttribute<MapControllerAttribute>();
            if (actionRoute.NotEmpty())
            {
                if (attr is MapGetAttribute)
                {
                    attributes.Add(new HttpGetAttribute());
                    attributes.Add(new RouteAttribute(actionRoute));
                }
                else if (attr is MapPostAttribute)
                {
                    attributes.Add(new HttpPostAttribute());
                    attributes.Add(new RouteAttribute(actionRoute));
                }
                controller.AddPageAction(type, actionRoute, attributes.ToArray());
            }
        }
    }
    /// <summary>
    /// 添加分页列表接口
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="types"></param>
    /// <param name="attributes"></param>
    private static void AddGetAction(DynamicController controller, IEnumerable<Type> types, List<Attribute>? attributes = null)
    {
        if (types.IsEmpty())
            return;
        attributes = new List<Attribute>();
        foreach (var type in types)
        {
            var actionRoute = type.GetCustomAttribute<MapControllerAttribute>()!.ActionName;
            if (actionRoute.NotEmpty())
                controller.AddGetAction(type, actionRoute, attributes.ToArray());
        }
    }
}