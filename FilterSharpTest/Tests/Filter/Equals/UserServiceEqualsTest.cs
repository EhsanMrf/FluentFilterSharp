using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.Tests.FilterTest.Equals;

/// <summary>
/// only test on equals
/// </summary>
public class UserServiceEqualsTest(TestFixture fixture) :UserSharedService(fixture)
{
    
    /// <summary>
    /// FilterOperatorNames.Equals = 'equals'
    /// </summary>
    [Fact]
    public async Task GetUserByName_WhenEqualsFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "John"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);


        data.Items.Should().NotBeNull();
        data.Items.As<List<User>>().First().Name.Should().Be("John");
    }  
    
    /// <summary>
    /// pattern = ((x.Name == "John") AndAlso (x.Age == 28))
    /// default logic = and
    /// FilterRequestInstance("Name", FilterOperatorNames.Equals, "Jennifer", logic= and  )
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenEqualsAndLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "John"))
            .AddFilter(FilterRequestInstance("LastName", FilterOperatorNames.Equals, "Smith"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.Items.As<List<User>>().First().Name.Should().Be("John");
        data.Items.As<List<User>>().First().LastName.Should().Be("Smith");
    }
    
    /// <summary>
    /// pattern = ((x.Name == "Jac") OrElse (x.Name == "Jennifer"))
    ///  logic set = Or
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenEqualsOrLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "Jac", FilterLogicalNames.LogicOr))
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "Jennifer"))
            .Build();
        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.Items.Should().NotBeNull();
        data.Items.As<List<User>>().First().Name.Should().Be("Jennifer");
        data.TotalCount.Should().BeGreaterThan(0);
    }
}