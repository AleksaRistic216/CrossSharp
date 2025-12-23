namespace CrossSharp.Utils.Interfaces;

public interface IIconProvider
{
    string GetSvg(Icon icon);

    bool TryGetSvg(Icon icon, out string svg);

    IEnumerable<Icon> GetAvailableIcons();
}
