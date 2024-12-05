using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.Equals;

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
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().Be("John");
    }  
    
    /// <summary>
    /// pattern = ((x.Name == "John") AndAlso (x.Age == 28))
    /// default logic = and
    /// FilterRequestInstance("Age", FilterOperatorNames.Equals, "28", logic= and  )
    /// </summary>
    [Fact]
    public async Task GetUserByNameAndAge_WhenEqualsFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "John"))
            .AddFilter(FilterRequestInstance("Age", FilterOperatorNames.Equals, "28"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().Be("John");
        data.items.First().Age.Should().Be(28);
    }
    
    /// <summary>
    /// pattern = ((x.Name == "Jac") OrElse (x.Name == "Jennifer"))
    ///  logic set = Or
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenEqualsFilterIsUsed_And_LogicEquals_Or()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "Jac", FilterLogicalNames.LogicOr))
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.Equals, "Jennifer"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().Be("Jennifer");
        data.totalCount.Should().BeGreaterThan(0);
    }
}