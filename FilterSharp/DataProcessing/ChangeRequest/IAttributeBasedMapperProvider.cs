using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;

namespace FilterSharp.DataProcessing.ChangeRequest;

public interface IAttributeBasedMapperProvider
{
    IEnumerable<FilterSharpMapper>? GetListSharpMapper<T>(AbstractFilterSharpMapper<T>? sharpMapper) where T : class;
}