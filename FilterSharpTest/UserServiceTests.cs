using FilterSharp.Input;
using FilterSharp.Input.Builder;
using FilterSharpTest.Fixture;
using FluentAssertions;
using FilterSharpTest.Shared;

namespace FilterSharpTest;

public class UserServiceTests(TestFixture fixture) : UserSharedService(fixture)
{

}