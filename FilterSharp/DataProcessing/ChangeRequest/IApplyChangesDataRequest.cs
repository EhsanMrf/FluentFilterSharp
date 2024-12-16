using FilterSharp.Input;

namespace FilterSharp.DataProcessing;

public interface IApplyChangesDataRequest
{
    void GetDataChange<T>(DataQueryRequest? queryRequest) where T : class;
}