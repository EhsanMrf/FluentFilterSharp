using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.ExtendBehavior;

public abstract class AbstractBehaviorDataRequestProcessor
{ 
    public abstract void ExceptionHandler<T>(List<FilterRequest> filterRequests, List<FilterSharpMapper> filterSharps, FilterSharpMapperBuilder<T> builder);
}