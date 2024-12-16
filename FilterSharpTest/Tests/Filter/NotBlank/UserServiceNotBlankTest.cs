using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.Filter.NotBlank;

public class UserServiceNotBlankTest(TestFixture fixture):UserSharedService(fixture)
{
    [Fact]
    public async Task GetUsers_WhenBlankFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotBlank, ""))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
    }
}