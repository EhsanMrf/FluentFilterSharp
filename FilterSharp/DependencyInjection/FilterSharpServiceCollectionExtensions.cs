using FilterSharp.DataProcessing;
using FilterSharp.DataProcessing.ChangeRequest;
using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.DataProcessing.ProcessorRequest.ChangeFields;
using FilterSharp.DataProcessing.ProcessorRequest.ChangeFields.Filter;
using FilterSharp.DataProcessing.ProcessorRequest.ChangeFields.Sort;
using FilterSharp.DataProcessing.Sorting;
using FilterSharp.DependencyInjection.Locator;
using FilterSharp.ExtendBehavior;
using FilterSharp.Filter.Operator.Register;
using FilterSharp.FluentSharp;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharp.DependencyInjection;

public static class FilterSharpServiceCollectionExtensions
{
    public static IServiceCollection AddFilterSharp(this IServiceCollection services,Action<PaginationOptions>? configurePagination = null)
    {
        services.InjectPaginationOption(configurePagination);
        services.AddSingleton<IDataQueryProcessor, DataQueryProcessor>();

        services.AddScoped<IRequestFieldsChange,SortRequestProcessor>();
        services.AddScoped<IRequestFieldsChange,FilterRequestFieldsChange>();
        
        services.AddSingleton<IApplyChangesDataRequest, ApplyChangesDataRequest>();
        services.AddSingleton<IDataPaginationService, DataPaginationService>();
        services.AddSingleton<IDataFilterService, DataFilterService>();
        services.AddSingleton<IDataSortingService, DataSortingService>();


        services.AddSingleton<IMapperConfigurator, MapperConfigurator>();
        services.AddSingleton<IAttributeBasedMapperProvider, AttributeBasedMapperProvider>();
        
        services.AddScoped<IDataRequestProcessor, RequestTransformationProcessor>();
        
        services.InjectFilterSharpMappers();
        services.InjectExtendBehaviorException();
        FilterStrategyRegistry.RegisterAllStrategies();
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
}