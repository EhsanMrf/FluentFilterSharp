using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.Filter.LessThan;

public class UserServiceLessThanTest(TestFixture testFixture):UserSharedService(testFixture)
{
    /// <summary>
    /// pattern (x=>x.Age < number)
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenAgeLessThanFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance(nameof(User.Age), FilterOperatorNames.LessThan, "30"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().All(u => u.Age < 30).Should().BeTrue();
    }

}

