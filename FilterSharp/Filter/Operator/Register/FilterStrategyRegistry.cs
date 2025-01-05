using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.TransActionService;

namespace FilterSharp.Filter.Operator.Register;

internal class FilterStrategyRegistry : IDisposable
{
    internal IFilterStrategy? GetStrategy(string operatorName)
    {
        DictionaryRegisterFiltersType.GetTypeByOperatorName(operatorName, out var type);
        if (type==null) return null;
        return Activator.CreateInstance(type) as IFilterStrategy;
    }

    public void Dispose() { }
}