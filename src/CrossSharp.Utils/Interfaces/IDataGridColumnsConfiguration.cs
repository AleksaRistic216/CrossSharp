using SixLabors.Fonts;

namespace CrossSharp.Utils.Interfaces;

public interface IDataGridColumnConfiguration
{
    string? HeaderText { get; set; }
    ColorRgba HeaderBackgroundColor { get; set; }
    ColorRgba HeaderTextColor { get; set; }
    TextAlignment HeaderTextAlignment { get; set; }
    int Width { get; set; }
}
