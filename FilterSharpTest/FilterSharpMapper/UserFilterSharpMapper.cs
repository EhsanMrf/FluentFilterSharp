using FilterSharp.FluentSharp;
using FilterSharpTest.Model;

namespace FilterSharpTest.FilterSharpMapper;

public class UserFilterSharpMapper : AbstractFilterSharpMapper<User>
{
    public override void Configuration(FilterSharpMapperBuilder<User> builder)
    {
        builder.OnField(x => x.Name).FilterFieldName(nameof(User.Name));
        builder.AllowedSelects([nameof(User.Name), nameof(User.Age)]);
    }
}