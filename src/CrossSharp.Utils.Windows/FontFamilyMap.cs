using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Windows;

public class FontFamilyMap : IFontFamilyMap
{
    public string GetFontFamilyPath(FontFamily fontFamily)
    {
        if (fontFamily == FontFamily.Default)
            return GetFontFamilyPath(FontFamily.Corbel);

        return fontFamily switch
        {
            FontFamily.Corbel => @"C:\Windows\Fonts\Corbel.ttf",
            FontFamily.Default => throw new Exception(
                "This should never happen, handle default one by calling this method recursively with font you want to be mapped to default"
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(fontFamily), fontFamily, null),
        };
    }
}
