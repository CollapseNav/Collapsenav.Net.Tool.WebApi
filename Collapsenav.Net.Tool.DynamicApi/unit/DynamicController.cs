using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Collapsenav.Net.Tool.DynamicApi;

/// <summary>
/// 生成动态controller的节点
/// </summary>
public class DynamicController
{
    private readonly ControllerModel? controllerModel;

    /// <summary>
    /// 动态api路由的规则
    /// </summary>
    public DynamicApiConfig? Config { get; set; } = DynamicApiConfig.GlobalConfig;
    public DynamicController(ControllerModel controllerModel)
    {
        this.controllerModel = controllerModel;
        ControllerType = controllerModel.ControllerType;
        Route = new RouteAttribute(controllerModel.ControllerName);
        Actions.AddRange(controllerModel.Actions.Select(item => new DynamicAction(item)));
    }
    public DynamicController(Type controllerType)
    {
        ControllerType = controllerType;
        Actions.AddRange(controllerType.GetMethods().Select(item => DynamicAction.GetDynamicAction(item)).Where(item => item != null).Select(item => item!));
    }
    public DynamicController(Type controllerType, string route) : this(controllerType)
    {
        Route = new RouteAttribute(route);
    }

    public Type? ControllerType { get; set; }
    public RouteAttribute? Route { get; set; }
    public List<DynamicAction> Actions { get; set; } = new();

    public ControllerModel? GetControllerModel(ApplicationModel? applicationModel = null)
    {
        if (ControllerType == null || Route == null)
            return null;
        var attrs = ControllerType.GetCustomAttributes().ToList();
        var cm = controllerModel ?? new ControllerModel(ControllerType.GetTypeInfo(), attrs)
        {
            ControllerName = Route.Template
        };
        Actions?.ForEach(action => action.GetActionModel(cm));
        // 配置controller路由
        Config?.ConfigControllerRoute(cm);
        // 标记为ApiController
        cm.Filters.Add(new ApiControllerAttribute());

        // 配置api是否可被 swagger 发现
        if (!cm.ApiExplorer.IsVisible.HasValue)
            cm.ApiExplorer.IsVisible = true;
        foreach (var action in cm.Actions)
        {
            if (!action.ApiExplorer.IsVisible.HasValue)
                action.ApiExplorer.IsVisible = true;
        }

        foreach (var prop in ControllerType.Props())
            cm.ControllerProperties.Add(new PropertyModel(prop, prop.GetCustomAttributes().ToList()));

        if (applicationModel != null && !applicationModel.Controllers.Any(i => i == cm))
        {
            cm.Application = applicationModel;
            applicationModel.Controllers.Add(cm);
        }
        return cm;
    }
}