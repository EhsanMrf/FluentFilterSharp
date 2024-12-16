using System.ComponentModel.DataAnnotations;
using FilterSharp.Attribute;

namespace FilterSharpTest.Model;

public sealed class User
{
    public int Id { get; private set; }
    [MaxLength(50)] public string Name { get; private set; } = null!;
    [MaxLength(100)] public string LastName { get; private set; } = null!;
    public byte Age { get; private set; }
    public Guid Code { get; private set; }

    private User()
    {
    }

    private User(string name, string lastName, byte age, Guid code)
    {
        Name = name;
        LastName = lastName;
        Age = age;
        Code = code;
    }

    public static User Instance(string name, string lastName, byte age, Guid code)
    {
        return new User(name, lastName, age, code);
    }
}