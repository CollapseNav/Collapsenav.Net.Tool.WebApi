using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Collapsenav.Net.Tool.DynamicApi;
public class ApplicationServiceConvention : IApplicationModelConvention
{
    /// <summary>
    /// 需要动态构建路由的controller类型，一般来说是泛型类
    /// </summary>
    public static List<DynamicController> DynamicControllers = new();
    public void Apply(ApplicationModel application)
    {
        // 移除重复类型
        DynamicControllers.RemoveAll(item => application.Controllers.Any(t => t.ControllerType.FullName == item.ControllerType!.FullName));
        // 将标记的class加入集合
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.IsDynamicApi() && !DynamicControllers.Any(i => i.ControllerType!.FullName == controller.ControllerType.FullName))
                DynamicControllers.Add(new DynamicController(controller));
        }
        // 统一处理
        foreach (var con in DynamicControllers)
            con.GetControllerModel(application);
    }
}