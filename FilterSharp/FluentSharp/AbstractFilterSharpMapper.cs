namespace FilterSharp.FluentSharp;

public abstract class AbstractFilterSharpMapper<T> where T : class
{
    public abstract void Configuration(FilterSharpMapperBuilder<T> builder);
}