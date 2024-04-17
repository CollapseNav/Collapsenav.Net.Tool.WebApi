namespace Collapsenav.Net.Tool.DynamicApi;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class MapControllerAttribute : Attribute
{
    public MapControllerAttribute(string value, string? actionName = null, bool isPage = true)
    {
        Value = value;
        ActionName = actionName;
        IsPage = isPage;
    }
    /// <summary>
    /// 控制器名称
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// 方法名称
    /// </summary>
    public string? ActionName { get; }
    /// <summary>
    /// 是否为分页方法
    /// </summary>
    public bool IsPage { get; }
}