using FilterSharp.FluentSharp;

namespace FilterSharp.DataProcessing.Mapp;

public interface IMapperConfigurator
{
    FilterSharpMapperBuilder<T> Configure<T>(AbstractFilterSharpMapper<T> filterSharpMapper) where T : class;
}