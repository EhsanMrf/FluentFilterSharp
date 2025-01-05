using FilterSharp.Attribute;
using FilterSharp.Filter.Operator.ContractFilter;

namespace FilterSharp.Filter.Operator.Register
{
    internal static class DictionaryRegisterFiltersType
    {
        private static readonly Lazy<Dictionary<string, Type>> StrategyTypesLazy = new(RegisterAllStrategies);

        private static Dictionary<string, Type> RegisterAllStrategies()
        {
            var strategyType = typeof(IFilterStrategy);
            var strategyTypes = new Dictionary<string, Type>();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => strategyType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttributes(typeof(OperatorNameAttribute), false)
                    .Cast<OperatorNameAttribute>()
                    .FirstOrDefault();

                if (attribute != null)
                {
                    strategyTypes[attribute.Name] = type;
                }
            }

            return strategyTypes;
        }

        public static void GetTypeByOperatorName(string key, out Type? type)
        {
            var strategyTypes = StrategyTypesLazy.Value; // Initialize once
            strategyTypes.TryGetValue(key, out type);
        }
    }
}