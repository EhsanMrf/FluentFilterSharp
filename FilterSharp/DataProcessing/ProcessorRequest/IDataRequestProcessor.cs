using FilterSharp.FluentSharp;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public interface IDataRequestProcessor
{
    void ApplyChanges<T>(DataRequest request, FilterSharpMapperBuilder<T> builder) where T : class;
}