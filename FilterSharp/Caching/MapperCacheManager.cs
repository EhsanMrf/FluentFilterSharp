using FilterSharp.FluentSharp;

namespace FilterSharp.Caching;

public class MapperCacheManager
{
    private readonly Dictionary<Type, object> _mappersCache = new();

    public MapperCacheManager(IServiceProvider serviceProvider)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies();
        var mapperTypes = assembly
            .SelectMany(a => a.GetTypes())
            .Where(t => t is {IsAbstract: false, BaseType.IsGenericType: true}
                        && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractFilterSharpMapper<>));

        foreach (var mapperType in mapperTypes)
        {
            var mapperInterface = mapperType.BaseType!;
            var instance = serviceProvider.GetService(mapperType);
            if (instance != null)
            {
                _mappersCache[mapperInterface] = instance;
            }
        }
    }

    public AbstractFilterSharpMapper<T>? GetMapper<T>() where T : class
    {
        var type = typeof(AbstractFilterSharpMapper<T>);
        return _mappersCache.TryGetValue(type, out var mapper) ? mapper as AbstractFilterSharpMapper<T> : null;
    }
}
