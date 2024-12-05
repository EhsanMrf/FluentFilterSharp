using FilterSharpTest.Model;
using Microsoft.EntityFrameworkCore;

namespace FilterSharpTest.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}