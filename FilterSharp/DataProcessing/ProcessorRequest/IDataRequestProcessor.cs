using FilterSharp.FluentSharp;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public interface IDataRequestProcessor
{
    void ApplyChanges<T>(DataQueryRequest queryRequest, FilterSharpMapperBuilder<T> builder) where T : class;
}