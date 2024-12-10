using FilterSharp.DataProcessing;
using FilterSharp.Input;
using FilterSharp.Input.Builder;
using FilterSharpTest.Database;
using FilterSharpTest.Fixture;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharpTest.Shared;

public abstract class UserSharedService : IClassFixture<TestFixture>
{
    protected readonly AppDbContext Context;
    protected readonly IDataQueryProcessor DataProcessor;

    protected UserSharedService(TestFixture fixture)
    {
        var configureServices = Startup.ConfigureServices();
        Context = fixture.Context;
        DataProcessor = configureServices.GetService<IDataQueryProcessor>()!;
    }

    internal DataQueryRequestBuilder RequestBuilder() => new();

    internal FilterRequest FilterRequestInstance(string field, string @operator, string value,string? logic=null,IEnumerable<FilterRequest>? filters=null)
    {
        return logic != null ? FilterRequest.Create(field, @operator, value,logic,filters) 
            : FilterRequest.Create(field, @operator, value);
    }
}