using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.Filter.StartsWith;

public class UserServiceStartsWithTest(TestFixture testFixture) : UserSharedService(testFixture)
{
    [Fact]
    public async Task GetUsers_WhenStartsWithFilterMatches()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance(nameof(User.Name), FilterOperatorNames.StartsWith, "Jo"))
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().All(u => u.Name.StartsWith("Jo")).Should().BeTrue();
    }
    
    [Fact]
    public async Task GetUsers_WhenStartsWithFilterDoesNotMatch()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance(nameof(User.Name), FilterOperatorNames.StartsWith, "Xyz"))
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().Be(0);
        data.Items.As<List<User>>().Should().HaveCount(0);
    }

}