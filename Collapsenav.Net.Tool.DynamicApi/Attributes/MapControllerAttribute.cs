namespace Collapsenav.Net.Tool.DynamicApi;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class MapControllerAttribute : System.Attribute
{
    public MapControllerAttribute(string value)
    {
        Value = value;
    }
    public string Value { get; }
}