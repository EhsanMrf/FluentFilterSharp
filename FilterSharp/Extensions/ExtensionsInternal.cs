namespace FilterSharp.Extensions;

internal static class ExtensionsInternal
{
    internal static void Guard(this object obj, string message, string paramName) {
        if (obj == null) {
            throw new ArgumentNullException(paramName, message);
        }
    }
}