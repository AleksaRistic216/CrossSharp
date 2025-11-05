using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Themes;

static class Constants
{
    internal const int ANTI_ALIASING_LEVEL = 16;
    internal const int DEFAULT_FONT_SIZE = 16;
    internal const FontFamily DEFAULT_FONT_FAMILY = FontFamily.Default;
    internal static readonly ColorRgba DefaultLayoutBackgroundColorLight = ColorRgba.WhiteSmoke;
    internal static readonly ColorRgba DefaultLayoutBackgroundColorDark = ColorRgba.DimGray;

    internal const int DEFAULT_FLAT_LAYOUT_ITEM_SPACING = 0;
    internal const int DEFAULT_CONTAINED_LAYOUT_ITEM_SPACING = 0;
    internal const int DEFAULT_ROUNDED_LAYOUT_ITEM_SPACING = 8;

    internal const int DEFAULT_FLAT_CORNER_RADIUS = 0;
    internal const int DEFAULT_CONTAINED_CORNER_RADIUS = 0;
    internal const int DEFAULT_ROUNDED_CORNER_RADIUS = 8;
}
