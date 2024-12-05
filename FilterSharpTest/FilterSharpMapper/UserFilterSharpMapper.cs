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
            op.CanSort = false;
            op.CanFilter = true;
            op.CanOperatorNames = [FilterOperator.Contains, FilterOperator.Equals];
        });
    }
}