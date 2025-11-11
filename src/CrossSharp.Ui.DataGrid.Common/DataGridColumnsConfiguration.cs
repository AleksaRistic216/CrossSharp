using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;
using SixLabors.Fonts;

namespace CrossSharp.Ui.Common;

public class DataGridColumnConfiguration : IDataGridColumnConfiguration
{
    public string? HeaderText { get; set; }
    public ColorRgba HeaderBackgroundColor { get; set; } = ColorRgba.Transparent;
    public ColorRgba HeaderTextColor { get; set; } = ColorRgba.Black;
    public TextAlignment HeaderTextAlignment { get; set; } = TextAlignment.Center;
    public int Width { get; set; } = 200;
}
