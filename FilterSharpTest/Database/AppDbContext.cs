using FilterSharp.DataProcessing;
using FilterSharpTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharpTest.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public readonly IDataQueryProcessor DataProcessor;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        var configureServices = Startup.ConfigureServices();
        DataProcessor = configureServices.GetService<IDataQueryProcessor>()!;
    }
}