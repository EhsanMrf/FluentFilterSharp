using FilterSharpTest.Database;
using FilterSharpTest.Fixture;
using FilterSharpTest.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FilterSharp.DependencyInjection;
namespace FilterSharpTest;

public class Startup
{
    public static ServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();
            
        serviceCollection.AddMyFilterSharpServices();

        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("DataSource=:memory:"));
        
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<TestFixture>();

        return serviceCollection.BuildServiceProvider();
    }
}