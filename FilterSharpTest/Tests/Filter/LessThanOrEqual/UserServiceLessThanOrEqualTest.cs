using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.LessThanOrEqual;

public class UserServiceLessThanOrEqualTest(TestFixture testFixture):UserSharedService(testFixture)
{
    /// <summary>
    /// pattern (x=>x.Age <= number)
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenAgeLessThanOrEqualFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Age", FilterOperatorNames.LessThanOrEqual, "40"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);
        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().All(u => u.Age <= 40).Should().BeTrue();
    }
}