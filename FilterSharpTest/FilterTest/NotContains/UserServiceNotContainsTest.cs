using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.NotContains;

public class UserServiceNotNotContainsTest(TestFixture testFixture):UserSharedService(testFixture)
{
    /// <summary>
    /// FilterOperatorNames.NotContains = 'notContains'
    /// pattern  Name.NotContains("Aurelius")
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenNotContainsFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotContains, "Aurelius"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.totalCount.Should().BeGreaterThan(0);
    }
    
    /// <summary>
    /// pattern = (x.Name.NotContains("Aurelius") AndAlso (x.LastName == "Cornelius"))
    /// default logic = and
    /// FilterRequestInstance("LastName", FilterOperatorNames.Equals, "Cornelius", logic= and  )
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenNotContainsAndLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotContains, "Aurelius"))
            .AddFilter(FilterRequestInstance("LastName", FilterOperatorNames.Equals, "Cornelius"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.totalCount.Should().BeGreaterThan(0);
    }
}