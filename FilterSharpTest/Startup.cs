using FilterSharpTest.Database;
using FilterSharpTest.Fixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FilterSharp.DependencyInjection;

namespace FilterSharpTest;

public abstract  class Startup
{
    public static ServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddFilterSharp(options => { options.DefaultPageSize = 10; });

        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("DataSource=:memory:"));

        serviceCollection.AddScoped<TestFixture>();

        return serviceCollection.BuildServiceProvider();
    }
}