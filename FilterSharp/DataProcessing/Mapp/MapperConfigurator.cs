using FilterSharp.FluentSharp;

namespace FilterSharp.DataProcessing.Mapp;

public class MapperConfigurator : IMapperConfigurator
{
    public FilterSharpMapperBuilder<T> Configure<T>(AbstractFilterSharpMapper<T> filterSharpMapper) where T : class
    {
        var instance = filterSharpMapper.DefineInstance();
        filterSharpMapper.Configuration(instance);
        return instance;
    }
}