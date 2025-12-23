using System.Reflection;
using CrossSharp.Icons.IconSets;

namespace CrossSharp.Icons;

public static class IconProvider
{
    private static readonly Assembly _assembly = typeof(IconProvider).Assembly;
    private static readonly Dictionary<string, string> _cache = new();

    public static string GetSvg(IconSet iconSet, Icon icon)
    {
        var cacheKey = $"{iconSet}.{icon}";

        if (_cache.TryGetValue(cacheKey, out var cached))
        {
            return cached;
        }

        var resourceName = $"CrossSharp.Icons.IconSets.{iconSet}.{icon}.svg";
        var svg = LoadEmbeddedResource(resourceName);
        _cache[cacheKey] = svg;
        return svg;
    }

    public static bool TryGetSvg(IconSet iconSet, Icon icon, out string svg)
    {
        svg = GetSvg(iconSet, icon);
        return !string.IsNullOrEmpty(svg);
    }

    public static IEnumerable<Icon> GetAllIcons()
    {
        return Enum.GetValues<Icon>();
    }

    public static IEnumerable<IconSet> GetAllIconSets()
    {
        return Enum.GetValues<IconSet>();
    }

    private static string LoadEmbeddedResource(string resourceName)
    {
        using var stream = _assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return string.Empty;
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
