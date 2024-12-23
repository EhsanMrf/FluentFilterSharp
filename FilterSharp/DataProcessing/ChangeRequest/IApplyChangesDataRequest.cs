using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ChangeRequest;

public interface IApplyChangesDataRequest
{
    void ApplyDataChangesWithValidation<T>(DataQueryRequest? queryRequest) where T : class;
}