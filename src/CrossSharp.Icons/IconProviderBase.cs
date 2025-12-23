using System.Reflection;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Icons;

public abstract class IconProviderBase : IIconProvider
{
    private readonly Assembly _assembly;
    private readonly Dictionary<Icon, string> _cache = new();
    private readonly string _resourcePrefix;

    protected IconProviderBase(string iconSetName)
    {
        _assembly = GetType().Assembly;
        _resourcePrefix = $"CrossSharp.Icons.IconSets.{iconSetName}";
    }

    public string GetSvg(Icon icon)
    {
        if (_cache.TryGetValue(icon, out var cached))
        {
            return cached;
        }

        var resourceName = $"{_resourcePrefix}.{icon}.svg";
        var svg = LoadEmbeddedResource(resourceName);
        _cache[icon] = svg;
        return svg;
    }

    public bool TryGetSvg(Icon icon, out string svg)
    {
        svg = GetSvg(icon);
        return !string.IsNullOrEmpty(svg);
    }

    public virtual IEnumerable<Icon> GetAvailableIcons()
    {
        return Enum.GetValues<Icon>();
    }

    private string LoadEmbeddedResource(string resourceName)
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
