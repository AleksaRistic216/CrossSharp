using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ITheme
{
    RenderStyle Style { get; set; }
    int DefaultFontSize { get; set; }
    FontFamily DefaultFontFamily { get; set; }

    /// <summary>
    /// Used as the background color for layouts elements like StaticLayout, StackedLayout, etc.
    /// </summary>
    ColorRgba LayoutBackgroundColor { get; set; }

    /// <summary>
    /// Used for primary interactive elements like buttons, title bar, accordions, etc.
    /// </summary>
    ColorRgba PrimaryColor { get; set; }
    ColorRgba SecondaryColor { get; set; }
    int DefaultCornerRadius { get; set; }
    int DefaultLayoutItemSpacing { get; set; }
}
