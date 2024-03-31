namespace Collapsenav.Net.Tool.DynamicApi;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class MapControllerAttribute : System.Attribute
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
    public bool IsPage { get; }
}