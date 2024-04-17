using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Collapsenav.Net.Tool.DynamicApi;

/// <summary>
/// 动态action
/// </summary>
public class DynamicAction
{
    private readonly ActionModel? actionModel;
    public DynamicApiConfig? Config { get; set; } = DynamicApiConfig.GlobalConfig;
    public MethodInfo? MethodInfo { get; }
    public RouteAttribute? Route { get; set; }
    public List<Attribute> ExtraAttributes { get; } = new();

    public DynamicAction(ActionModel actionModel, Attribute[]? extraAttributes = null) : this(actionModel.ActionMethod, string.Empty, extraAttributes)
    {
        this.actionModel = actionModel;
    }
    public DynamicAction(MethodInfo methodInfo, string? route = null, Attribute[]? extraAttributes = null)
    {
        MethodInfo = methodInfo;
        if (route.NotEmpty())
            Route = new RouteAttribute(route!);
        if (extraAttributes.NotEmpty())
            ExtraAttributes.AddRange(extraAttributes);
    }

    public static DynamicAction? GetDynamicAction(MethodInfo info)
    {
        // 只接受4种httpmethod
        if (!info.HasAttribute(typeof(HttpGetAttribute), typeof(HttpPostAttribute), typeof(HttpPutAttribute), typeof(HttpDeleteAttribute)))
            return null;
        return new DynamicAction(info);
    }

    public ActionModel? GetActionModel(ControllerModel? controllerModel = null)
    {
        if (MethodInfo == null)
            return null;
        ExtraAttributes.AddRange(MethodInfo.GetCustomAttributes());
        Route ??= ExtraAttributes.FirstOrDefault(i => i is RouteAttribute) as RouteAttribute;
        var action = actionModel ?? new ActionModel(MethodInfo, ExtraAttributes)
        {
            ActionName = Route?.Template ?? MethodInfo.Name
        };
        foreach (var p in MethodInfo.GetParameters())
        {
            var bi = BindingInfo.GetBindingInfo(p.GetCustomAttributes());
            var pm = new ParameterModel(p, p.GetCustomAttributes().ToList())
            {
                ParameterName = p.Name ?? string.Empty,
                Action = action,
                BindingInfo = bi
            };
            action.Parameters.Add(pm);
        }
        Config?.ConfigActionRoute(action);
        if (action.Selectors.NotEmpty() && action.Selectors.First().EndpointMetadata.IsEmpty())
            action.Selectors.First().EndpointMetadata.AddRange(ExtraAttributes);
        action.ConfigureParameters();
        if (controllerModel != null && !controllerModel.Actions.Any(i => i == action))
        {
            action.Controller = controllerModel;
            controllerModel.Actions.Add(action);
        }
        return action;
    }
}