using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.FilterTest.Blank;

public class UserServiceBlankTest(TestFixture fixture):UserSharedService(fixture)
{
    [Fact]
    public async Task GetUsers_WhenBlankFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Blank, ""))
            .Build();
        
        var data=await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().Be(0);
        data.Items.As<List<User>>().All(u => string.IsNullOrEmpty(u.Name)).Should().BeTrue();
    }
}