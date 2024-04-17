namespace Collapsenav.Net.Tool.DynamicApi;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class MapGetAttribute : MapControllerAttribute
{
    public MapGetAttribute(string value, string? actionName = null, bool isPage = true) : base(value, actionName, isPage)
    {
    }
}