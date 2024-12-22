using FilterSharp.Attribute;
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

    internal static void RegisterAllStrategies()
    {
        var strategyTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IFilterStrategy).IsAssignableFrom(t));

        foreach (var type in strategyTypes)
        {
            var attribute = type.GetCustomAttributes(typeof(OperatorNameAttribute), false)
                .Cast<OperatorNameAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                var instance = (IFilterStrategy)Activator.CreateInstance(type)!;
                Strategies[attribute.Name] = instance;
            }
        }
    }
}