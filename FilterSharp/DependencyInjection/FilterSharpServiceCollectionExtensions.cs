using System.Reflection;
using FilterSharp.DataProcessing;
using FilterSharp.DataProcessing.ChangeRequest;
using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.DataProcessing.ProcessorRequest.ProcessFields;
using FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Filter;
using FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Select;
using FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Sort;
using FilterSharp.DataProcessing.Sorting;
using FilterSharp.DependencyInjection.Locator;
using FilterSharp.ExtendBehavior;
using FilterSharp.Filter.Operator.Register;
using FilterSharp.FluentSharp;
using FilterSharp.Select;
using FilterSharp.TransActionService;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharp.DependencyInjection;

public static class FilterSharpServiceCollectionExtensions
{
    public static IServiceCollection AddFilterSharp(this IServiceCollection services,Action<PaginationOptions>? configurePagination = null)
    {
        //FilterStrategyRegistry.RegisterAllStrategies();

        services.InjectPaginationOption(configurePagination);
        
        services.AddSingletonServices(Assembly.GetExecutingAssembly());
        services.AddScopedServices(Assembly.GetExecutingAssembly());

        services.InjectFilterSharpMappers();
        services.InjectExtendBehaviorException();
        ServiceLocator.DataQueryProcessor= services.BuildServiceProvider().GetRequiredService<IDataQueryProcessor>()!;

        return services;
    }

    private static void InjectFilterSharpMappers(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var mapperTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true } &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(AbstractFilterSharpMapper<>));

        foreach (var mapperType in mapperTypes)
        {
            services.AddScoped(mapperType);
            
            var baseType = mapperType.BaseType;
            if (baseType != null)
                services.AddScoped(baseType, mapperType);
        }
    }
    private static void InjectExtendBehaviorException(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        var processorTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(AbstractBehaviorDataRequestProcessor)));
        
        foreach (var processorType in processorTypes)
            services.AddSingleton(typeof(AbstractBehaviorDataRequestProcessor), processorType);
    }

    private static void InjectPaginationOption(this IServiceCollection services,Action<PaginationOptions>? configurePagination = null)
    {
        var paginationOptions = new PaginationOptions();
        configurePagination?.Invoke(paginationOptions);
    
        services.AddSingleton(paginationOptions);
    }

    private static void AddScopedServices(this IServiceCollection services, Assembly assembly)
    {
        var scopedTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && typeof(IScopeService).IsAssignableFrom(type));
        foreach (var type in scopedTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i != typeof(IScopeService));

            if (interfaceType != null) 
                services.AddScoped(interfaceType, type);
            else services.AddScoped(type);
        }
    }  
    
    private static void AddSingletonServices(this IServiceCollection services, Assembly assembly)
    {
        var scopedTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && typeof(ISingletonService).IsAssignableFrom(type));
        foreach (var type in scopedTypes)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i != typeof(ISingletonService));

            if (interfaceType != null) 
                services.AddSingleton(interfaceType, type);
            else services.AddSingleton(type);
        }
    }
}