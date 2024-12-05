using FilterSharp.DataProcessing;
using FilterSharp.Input;
using FilterSharp.Input.Builder;
using FilterSharpTest.Database;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Service;
using FluentAssertions;
using FilterSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharpTest;

public class UserServiceTests : IClassFixture<TestFixture>
{
    private readonly AppDbContext _context;
    private readonly IDataQueryProcessor _dataProcessor;

    public UserServiceTests()
    {
        var configureServices = Startup.ConfigureServices();

        _dataProcessor = configureServices.GetService<IDataQueryProcessor>()!;
        var requiredService = configureServices.GetRequiredService<TestFixture>();
        _context = requiredService.Context;
    }

    [Fact]
    public async Task GetUser_With_Equals()
    {
        var queryRequest = new DataQueryRequestBuilder()
            .AddFilter(FilterRequest.Create("Name", "equals", "John"))
            .SetPageNumber(1)
            .SetPageSize(10)
            .Build();
        var data = await _dataProcessor.ApplyQueryRequestAsync(_context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().Be("John");
    }
}