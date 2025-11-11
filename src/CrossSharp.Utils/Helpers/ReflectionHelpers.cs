namespace CrossSharp.Utils.Helpers;

internal static class ReflectionHelpers
{
    public static IEnumerable<System.Reflection.PropertyInfo> GetAllPropertiesWithAttribute<TAttribute>(Type type)
        where TAttribute : Attribute
    {
        var properties = type.GetProperties(
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
        );
        foreach (var property in properties)
            if (Attribute.IsDefined(property, typeof(TAttribute)))
                yield return property;
    }

    public static TAttribute? GetCustomAttribute<TAttribute>(this System.Reflection.PropertyInfo propertyInfo)
        where TAttribute : Attribute
    {
        return (TAttribute?)Attribute.GetCustomAttribute(propertyInfo, typeof(TAttribute));
    }
}
