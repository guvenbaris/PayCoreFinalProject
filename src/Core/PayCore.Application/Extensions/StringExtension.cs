namespace PayCore.Application.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string text)
    {
        return (text == null || text.Trim().Length == 0);
    }
    public static bool IsNotNullOrEmpty(this string text)
    {
        return !(text == null || text.Trim().Length == 0);
    }

    public static bool IsNull(this object value)
    {
        return (value == null || Convert.IsDBNull(value));
    }
    public static bool IsNotNull(this object value)
    {
        return !(value == null || Convert.IsDBNull(value));
    }
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
    {
        return !value.IsNullOrEmpty();
    }
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
    {
        return (value.IsNull() || !value.Any());
    }
}
