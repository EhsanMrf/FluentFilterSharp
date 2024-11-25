namespace FilterSharp.FluentSharp;

public abstract class AbstractFilerSharpMapper<T> where T : class
{
    public abstract void Configuration(FilterSharpMapperBuilder<T> builder);
}