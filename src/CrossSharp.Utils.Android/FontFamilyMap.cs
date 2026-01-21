using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Android;

public class FontFamilyMap : IFontFamilyMap
{
    public string GetFontFamilyPath(FontFamily fontFamily)
    {
        // Android uses system fonts via Typeface, not file paths
        // Return empty string as AndroidGraphics uses Typeface directly
        return fontFamily switch
        {
            FontFamily.Default => "",
            FontFamily.DejaVuSans => "",
            FontFamily.Corbel => "",
            _ => ""
        };
    }
}
