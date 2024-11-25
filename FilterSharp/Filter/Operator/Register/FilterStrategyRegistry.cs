using FilterSharp.Filter.Operator.ContractFilter;

namespace FilterSharp.Filter.Operator.Register;

internal static class FilterStrategyRegistry 
{

    private static readonly Dictionary<string, IFilterStrategy> Strategies = new();
    internal static IFilterStrategy? GetStrategy(string operatorName)
    {
        Strategies.TryGetValue(operatorName, out var strategy);
        return strategy;
    }
}