using FilterSharpTest.Fixture;
using FilterSharpTest.Model;
using FilterSharpTest.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FilterSharpTest.Tests.Sort;

public class UserServiceAscendingTest(TestFixture testFixture):UserSharedService(testFixture)
{

    [Fact]
    public async Task GetUsers_With_Sort_ByLastName_Ascending()
    {
        var queryRequest = RequestBuilder()
            .AddSorting(nameof(User.LastName), true)
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().Select(x => x.LastName).Should().BeInAscendingOrder();
    } 
    
    [Fact]
    public async Task GetUsers_With_Sort_ByLastName_Descending()
    {
        var queryRequest = RequestBuilder()
            .AddSorting(nameof(User.LastName), false)
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.As<List<User>>().Select(x => x.LastName).Should().BeInDescendingOrder();
    }
    
    [Fact]
    public async Task GetUsers_With_Sort_ByName_Ascending_And_LastName_Descending()
    {
        var queryRequest = RequestBuilder()
            .AddSorting(nameof(User.Name), true) 
            .AddSorting(nameof(User.LastName), false) 
            .Build();

        var data = await Context.Users.ApplyQueryResult(queryRequest);

        var sortedUsers = await Context.Users
            .OrderBy(u => u.Name)         
            .ThenByDescending(u => u.LastName)
            .Take(15) 
            .ToListAsync();
        
        data.TotalCount.Should().BeGreaterThan(0);
        data.Items.Should().BeEquivalentTo(sortedUsers);
    }
}