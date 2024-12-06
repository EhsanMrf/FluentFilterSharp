using FilterSharp.StaticNames;
using FilterSharpTest.Fixture;
using FilterSharpTest.Shared;
using FluentAssertions;

namespace FilterSharpTest.FilterTest.NotEquals;

public class UserServiceNotEqualsTest(TestFixture testFixture):UserSharedService(testFixture)
{
    /// <summary>
    /// FilterOperatorNames.NotEquals = 'notEquals'
    /// </summary>
    [Fact]
    public async Task GetUserByName_WhenNotEqualsFilterIsUsed()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotEquals, "John"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().NotBe("John");
    }  
    
    /// <summary>
    /// pattern = ((x.Name != "John") AndAlso (x.LastName != "Smith"))
    /// default logic = and
    /// FilterRequestInstance("Name", FilterOperatorNames.NotEquals, "Smith", logic= and  )
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenEqualsAndLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotEquals, "John"))
            .AddFilter(FilterRequestInstance("LastName", FilterOperatorNames.NotEquals, "Smith"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().NotBe("John");
        data.items.First().LastName.Should().NotBe("Smith");
    }
    
    /// <summary>
    /// pattern = ((x.Name != "Jac") OrElse (x.Name != "Jennifer"))
    ///  logic set = Or
    /// </summary>
    [Fact]
    public async Task GetUsers_WhenNotEqualsOrLogicApplied()
    {
        var queryRequest = RequestBuilder()
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotEquals, "Jac", FilterLogicalNames.LogicOr))
            .AddFilter(FilterRequestInstance("Name", FilterOperatorNames.NotEquals, "Jennifer"))
            .Build();
        var data = await DataProcessor.ApplyQueryRequestAsync(Context.Users, queryRequest);

        data.items.Should().NotBeNull();
        data.items.First().Name.Should().NotBe("Jennifer");
        data.totalCount.Should().BeGreaterThan(0);
    }
}