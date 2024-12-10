using FilterSharp.DataProcessing;

namespace FilterSharp.DependencyInjection.Locator;

public class ServiceLocator
{
    public static IDataQueryProcessor DataQueryProcessor { get; set; } = null!;

}