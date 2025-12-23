using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Ui.Common;

partial class Chart
{
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            OnBackgroundColorChangedInternal();
        }
    }

    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
}
