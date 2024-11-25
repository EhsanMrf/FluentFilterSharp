using System.Linq.Expressions;
using FilterSharp.Input;

namespace FilterSharp.Filter.Operator.ContractFilter;

public interface IFilterStrategy
{
    Expression Apply(FilterContext context);
}