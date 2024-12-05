using Microsoft.Extensions.DependencyInjection;

namespace FilterSharp.DependencyInjection;

public static class ServiceLocator
{
    private static IServiceProvider _serviceProvider;

    public static void SetLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static T GetService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}