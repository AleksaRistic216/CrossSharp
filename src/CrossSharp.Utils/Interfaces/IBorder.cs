using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IBorder
{
    int BorderWidth { get; set; }
    ColorRgba BorderColor { get; set; }
}
