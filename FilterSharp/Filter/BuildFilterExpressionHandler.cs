using System.Linq.Expressions;
using FilterSharp.Input;

namespace FilterSharp.Filter;

public static class BuildFilterExpressionHandler
{
    internal static List<ConstantExpression> BuildFilterExpression(MemberExpression property,FilterRequest filterRequest)
    {
        return filterRequest.Value.Contains(',')
            ? BuildConstantExpressionsFromSplit(filterRequest.Value, property)
            : BuildSingleConstantExpression(filterRequest.Value, property);
    }

    private static List<ConstantExpression> BuildSingleConstantExpression(string value, MemberExpression property)
    {
        var convertedValue = ConvertToType(value, property.Type);
        return new List<ConstantExpression>(1) { Expression.Constant(convertedValue, property.Type) };
    }

    private static List<ConstantExpression> BuildConstantExpressionsFromSplit(string value, MemberExpression property)
    {
        var rangeValues = value.Split(",");

        if (rangeValues.Length != 2)
            throw new ArgumentException("Value for inRange must contain exactly two values separated by a comma.");

        var convertedValues = ConvertToType(rangeValues, property.Type);
        return new List<ConstantExpression>(2)
        {
            Expression.Constant(convertedValues[0], property.Type),
            Expression.Constant(convertedValues[1], property.Type)
        };
    }

    private static object[] ConvertToType(string[] values, Type targetType)
    {
        var convertedValues = new object[values.Length];
        for (var i = 0; i < values.Length; i++)
            convertedValues[i] = ValueConverter.ConvertValue(values[i], targetType)!;
        
        return convertedValues;
    }

    private static object ConvertToType(string value, Type targetType)
    {
        return ValueConverter.ConvertValue(value, targetType)!;
    }
}