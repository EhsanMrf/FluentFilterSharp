using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public interface IDataRequestProcessor
{
    void ApplyChanges(DataQueryRequest dataQueryRequest,ICollection<FilterSharpMapper>? sharpMappers);
}