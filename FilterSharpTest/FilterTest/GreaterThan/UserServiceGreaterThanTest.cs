using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;
namespace FilterSharpTest.FilterTest.GreaterThan;

public class UserServiceGreaterThanTest(TestFixture testFixture):UserSharedService(testFixture)
{
    
    [Fact]
    public async Task GetUsers_WhenAgeGreaterThanFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance(nameof(User.Age), FilterOperatorNames.GreaterThan, "30"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().All(u => u.Age > 30).Should().BeTrue();
    }

}