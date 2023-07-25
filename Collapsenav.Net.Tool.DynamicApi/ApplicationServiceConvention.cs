using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Collapsenav.Net.Tool.DynamicApi;
public class ApplicationServiceConvention : IApplicationModelConvention
{
    public static List<Type> Types = new List<Type>();
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.IsDynamicApi())
                ConfigureApplicationService(controller);
        }
        foreach (var type in Types)
        {
            var cm = new ControllerModel(type.GetTypeInfo(), type.GetCustomAttributes().ToList());
            cm.ControllerName = "testdy";
            CreateActionModel(cm);
            ConfigureApplicationService(cm);
            cm.Filters.Add(new ApiControllerAttribute());
            cm.Selectors.First().EndpointMetadata.AddRange(cm.Attributes.ToList());
            foreach (var prop in type.Props())
            {
                cm.ControllerProperties.Add(new PropertyModel(prop, prop.GetCustomAttributes().ToList()));
            }
            application.Controllers.Add(cm);
        }
    }

    private void CreateActionModel(ControllerModel cm)
    {
        foreach (var action in cm.ControllerType.GetMethods())
        {
            if (!action.HasAttribute(typeof(HttpGetAttribute), typeof(HttpPostAttribute), typeof(HttpPutAttribute), typeof(HttpDeleteAttribute)))
                continue;
            var am = new ActionModel(action, action.GetCustomAttributes().ToList());
            am.ActionName = action.Name;
            am.Controller = cm;
            cm.Actions.Add(am);
        }
    }

    private static void ConfigureApplicationService(ControllerModel controller)
    {
        // 配置api是否可被 swagger 发现
        if (!controller.ApiExplorer.IsVisible.HasValue)
            controller.ApiExplorer.IsVisible = true;
        foreach (var action in controller.Actions)
        {
            if (!action.ApiExplorer.IsVisible.HasValue)
                action.ApiExplorer.IsVisible = true;
        }
        // 构建 controller  route
        controller.BuildControllerRoute();
        // 构建 action route
        foreach (var action in controller.Actions)
        {
            // 创建 action route
            action.BuildRoute();
            if (action.Selectors.First().EndpointMetadata.IsEmpty())
            {
                var attrs = action.Attributes.ToList();
                action.Selectors.First().EndpointMetadata.AddRange(attrs.Take(3).ToList());
            }
            action.ConfigureParameters();
        }
    }
}