using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.Contains;

public class UserServiceContainsTest(TestFixture fixture) : UserSharedService(fixture)
{
    /// <summary>
    /// FilterOperatorNames.Contains = 'contains'
    /// pattern  Name.Contains("Aurelius")
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenContainsFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Contains, "Aurelius"))
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// pattern = (x.Name.Contains("Aurelius") AndAlso (x.Id == 20))
    /// default logic = and
    /// FilterRequestInstance("Id", FilterOperatorNames.Equals, "20", logic= and  )
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenContainsAndLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Contains, "Aurelius"))
            .AddFilter(FilterRequestInstance("Id", FilterOperatorNames.Equals, "20"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);
        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
    }
}