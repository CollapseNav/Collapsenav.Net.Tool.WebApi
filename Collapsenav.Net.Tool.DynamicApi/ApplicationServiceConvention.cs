using System.Reflection;
using Collapsenav.Net.Tool.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Collapsenav.Net.Tool.DynamicApi;
public class ApplicationServiceConvention : IApplicationModelConvention
{
    /// <summary>
    /// 需要动态构建路由的controller类型，一般来说是泛型类
    /// </summary>
    public static List<DynamicController> DynamicControllers = new();
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.IsDynamicApi())
                DynamicControllers.Add(new DynamicController(controller));
            // ConfigureApplicationService(controller);
        }
        foreach (var con in DynamicControllers)
        {
            con.GetControllerModel(application);
        }
        // DynamicControllers.Clear();
        // foreach (var type in Types)
        // {
        //     var cm = new ControllerModel(type.GetTypeInfo(), type.GetCustomAttributes().ToList());
        //     cm.ControllerName = "testdy";
        //     CreateActionModel(cm);
        //     ConfigureApplicationService(cm);
        //     // cm.Filters.Add(new ApiControllerAttribute());
        //     // cm.Selectors.First().EndpointMetadata.AddRange(cm.Attributes.ToList());
        //     // foreach (var prop in type.Props())
        //     // {
        //     //     cm.ControllerProperties.Add(new PropertyModel(prop, prop.GetCustomAttributes().ToList()));
        //     // }
        //     application.Controllers.Add(cm);
        // }
    }

    private void CreateActionModel(ControllerModel cm)
    {
        foreach (var action in cm.ControllerType.GetMethods())
        {
            if (!action.HasAttribute(typeof(HttpGetAttribute), typeof(HttpPostAttribute), typeof(HttpPutAttribute), typeof(HttpDeleteAttribute)))
                continue;
            var am = new ActionModel(action, action.GetCustomAttributes().ToList());
            am.ActionName = action.Name;
            foreach (var p in action.GetParameters())
            {
                var pm = new ParameterModel(p, p.GetCustomAttributes().ToList());
                pm.ParameterName = p.Name;
                pm.Action = am;
                var bi = BindingInfo.GetBindingInfo(p.GetCustomAttributes());
                pm.BindingInfo = bi;
                am.Parameters.Add(pm);
            }
            am.Controller = cm;
            cm.Actions.Add(am);
        }
        // var ms = cm.ControllerType.GetMethods().Where(item => item.IsGenericMethod && item.DeclaringType.IsType<IController>());
        // foreach (var (value, index) in ms.SelectWithIndex())
        // {
        //     var m = value.MakeGenericMethod(GetTypes.First());
        //     var am = new ActionModel(m, new object[] { new RouteAttribute($"get{index}"), new HttpGetAttribute() }.ToList());
        //     am.ActionName = $"get{index}";
        //     foreach (var p in m.GetParameters())
        //     {
        //         if (p.ParameterType.IsType<IBaseGet>())
        //         {
        //             // var pm = new ParameterModel(GetTypes.First(), p.GetCustomAttributes().ToList());
        //             // pm.ParameterName = p.Name;
        //             // pm.Action = am;
        //             // var bi = BindingInfo.GetBindingInfo(p.GetCustomAttributes());
        //             // pm.BindingInfo = bi;
        //             // am.Parameters.Add(pm);
        //         }
        //         else
        //         {
        //             var pm = new ParameterModel(p, p.GetCustomAttributes().ToList());
        //             pm.ParameterName = p.Name;
        //             pm.Action = am;
        //             var bi = BindingInfo.GetBindingInfo(p.GetCustomAttributes());
        //             pm.BindingInfo = bi;
        //             am.Parameters.Add(pm);
        //         }
        //     }
        //     am.Controller = cm;
        //     cm.Actions.Add(am);
        // }
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
            if (action.Selectors.NotEmpty() && action.Selectors.First().EndpointMetadata.IsEmpty())
            {
                var attrs = action.Attributes.ToList();
                action.Selectors.First().EndpointMetadata.AddRange(attrs.Take(3).ToList());
            }
            action.ConfigureParameters();
        }
    }
}