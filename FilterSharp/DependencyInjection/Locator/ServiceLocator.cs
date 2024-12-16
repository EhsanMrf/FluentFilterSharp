using FilterSharp.DataProcessing;

namespace FilterSharp.DependencyInjection.Locator;

public abstract class ServiceLocator
{
    public static IDataQueryProcessor DataQueryProcessor { get; set; } = null!;
}