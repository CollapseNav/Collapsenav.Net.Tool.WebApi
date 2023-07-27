using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Collapsenav.Net.Tool.DynamicApi;

public class DynamicAction
{
    private readonly ActionModel? actionModel;

    public DynamicApiConfig? Config { get; set; } = DynamicApiConfig.GlobalConfig;
    public DynamicAction(ActionModel actionModel) : this(actionModel.ActionMethod)
    {
        this.actionModel = actionModel;
    }
    public DynamicAction(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
    }

    public DynamicAction(MethodInfo methodInfo, string route) : this(methodInfo)
    {
        Route = new RouteAttribute(route);
    }

    public static DynamicAction? GetDynamicAction(MethodInfo info)
    {
        // 只接受4种httpmethod
        if (!info.HasAttribute(typeof(HttpGetAttribute), typeof(HttpPostAttribute), typeof(HttpPutAttribute), typeof(HttpDeleteAttribute)))
            return null;
        return new DynamicAction(info);
    }

    public MethodInfo? MethodInfo { get; }
    public RouteAttribute? Route { get; set; }
    public List<Attribute> ExtraAttributes { get; } = new();

    public ActionModel? GetActionModel(ControllerModel? controllerModel = null)
    {
        if (MethodInfo == null)
            return null;
        var attrs = MethodInfo.GetCustomAttributes().ToList();
        Route ??= attrs.FirstOrDefault(i => i is RouteAttribute) as RouteAttribute;
        var action = actionModel ?? new ActionModel(MethodInfo, MethodInfo.GetCustomAttributes().ToList())
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
            action.Selectors.First().EndpointMetadata.AddRange(attrs.ToList());
        action.ConfigureParameters();
        if (controllerModel != null && !controllerModel.Actions.Any(i => i == action))
        {
            action.Controller = controllerModel;
            controllerModel.Actions.Add(action);
        }
        return action;
    }
}