using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FilterSharp.Extensions;

public static class EnumExtension
{
    
    public static string? GetEnumDisplayName(this System.Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
        
        return attribute?.Name ?? value.ToString();
    }
}