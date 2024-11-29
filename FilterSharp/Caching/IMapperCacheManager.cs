using FilterSharp.FluentSharp;

namespace FilterSharp.Caching;

public interface IMapperCacheManager
{
    AbstractFilterSharpMapper<T>? GetMapper<T>() where T : class;
}