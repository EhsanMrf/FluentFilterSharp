using FilterSharp.FluentSharp;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.Mapp;

public class MapperConfigurator : IMapperConfigurator,ISingletonService
{
    public FilterSharpMapperBuilder<T> Configure<T>(AbstractFilterSharpMapper<T> filterSharpMapper) where T : class
    {
        var instance = filterSharpMapper.DefineInstance();
        filterSharpMapper.Configuration(instance);
        return instance;
    }
}