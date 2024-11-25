namespace FilterSharp.Attribute;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class OperatorNameAttribute(string name) : System.Attribute
{
    public string Name { get; } = name;
}