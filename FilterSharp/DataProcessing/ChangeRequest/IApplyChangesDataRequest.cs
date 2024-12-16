using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ChangeRequest;

public interface IApplyChangesDataRequest
{
    void GetDataChange<T>(DataQueryRequest? queryRequest) where T : class;
}