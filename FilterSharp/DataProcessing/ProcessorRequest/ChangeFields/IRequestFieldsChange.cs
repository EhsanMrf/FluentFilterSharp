using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest.ChangeFields;

public interface IRequestFieldsChange
{
    void ChangeFields(DataQueryRequest queryRequest, ICollection<FilterSharpMapper>? sharpMappers);
}