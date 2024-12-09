using FilterSharp.FluentSharp;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public interface IDataRequestProcessor
{
    void ApplyChanges<T>(DataQueryRequest queryRequest, FilterSharpMapperBuilder<T> entity) where T : class;
}