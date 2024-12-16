using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.Filter.GreaterThanOrEqual;

public class UserServiceLessGreaterThanOrEqualTest(TestFixture fixture):UserSharedService(fixture)
{
    [Fact]
    public async Task GetUsers_WhenAgeGreaterThanOrEqualFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Age", FilterOperatorNames.GreaterThanOrEqual, "60"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);


        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().All(u => u.Age >= 60).Should().BeTrue();
    }
}