using FilterSharp.Enum;
using FilterSharp.FluentSharp;
using FilterSharpTest.Model;

namespace FilterSharpTest.FilterSharpMapper;

public class UserFilterSharpMapper : AbstractFilterSharpMapper<User>
{
    public override void Configuration(FilterSharpMapperBuilder<User> builder)
    {
        builder.OnField(x => x.Name, op =>
        {
            op.CanFilter = true;
            op.CanOperatorNames = [FilterOperator.NotBlank, FilterOperator.Contains, FilterOperator.Equals];
        });
    }
}