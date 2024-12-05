using FilterSharpTest.Database;
using FilterSharpTest.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FilterSharpTest.Fixture;

public class TestFixture : IDisposable, IAsyncDisposable
{

    public AppDbContext Context { get; private set; }
    private SqliteConnection _connection;

    public TestFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;
        
        Context = new AppDbContext(options);
        Context.Database.EnsureCreated(); 
        SeedData(Context);
    }

   

    private static void SeedData(AppDbContext context)
    {
        
        var firstNames = new List<string>
        {
            "John", "Michael", "David", "James", "William", "Robert", "Joseph", "Charles", "Daniel", "Matthew",
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Susan", "Jessica", "Sarah", "Karen", "Nancy"
        };

        // List of last names (Last names list in Latin)
        var lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
            "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Roberts"
        };
        
        
        var users = Enumerable.Range(1, 100).Select(i =>
        {
            // Choose random first name and last name
            var firstName = firstNames[i % firstNames.Count];
            var lastName = lastNames[i % lastNames.Count];

            var age = (byte)(18 + (i % 50)); // Age between 18 and 67
            var code = Guid.NewGuid(); // Unique code for each user

            return User.Instance(firstName, lastName, age, code);
        }).ToList();

        context.Users.AddRange(users);
        context.SaveChanges();
    }
    
    
    public void Dispose()
    {
        Context?.Dispose();
        _connection?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await Context.DisposeAsync();
    }
}