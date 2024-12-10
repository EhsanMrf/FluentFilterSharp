namespace FilterSharp.Filter;

internal static class ValueConverter
{
    
    
    
    internal static object? ConvertValue(string? value, Type targetType)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        return targetType switch
        {
            { } t when t == typeof(int) => int.Parse(value),
            { } t when t == typeof(double) => double.Parse(value),
            { } t when t == typeof(bool) => bool.Parse(value),
            { } t when t == typeof(Guid) => Guid.Parse(value),
            { } t when t == typeof(byte) => byte.Parse(value),
            { } t when t == typeof(float) => float.Parse(value),
            { } t when t == typeof(DateTime) => DateTime.Parse(value),
            _ => value // Assume it's a string or compatible type
        };
    }
}
