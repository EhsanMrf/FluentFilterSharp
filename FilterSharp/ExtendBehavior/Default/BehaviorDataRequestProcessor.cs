using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.ExtendBehavior.Default;

public class BehaviorDataRequestProcessor :AbstractBehaviorDataRequestProcessor
{
    public override void ExceptionHandler<T>(List<FilterRequest> filterRequests, List<FilterSharpMapper> filterSharps, FilterSharpMapperBuilder<T> entity)
    {
        throw new NotImplementedException("Mrf Error");
    }
}