using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.InRange;

public class UserServiceInRangeTest(TestFixture testFixture): UserSharedService(testFixture)
{
    /// <summary>
    /// pattern (x=>x.Age > number)
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenAgeInRangeIsUsed_ReverseRange()
    {
        var users = Context.Users.ToList();
        var queryRequest = RequestBuilder()
          
            .AddFilter(FilterRequestInstance(nameof(User.Age),FilterOperatorNames.InRange,"18,24")) 
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
    }
}