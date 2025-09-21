using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

public partial class PanelControl
{
    #region private
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    #endregion
    #region exposed
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            RaiseBackgroundColorChanged();
            Redraw();
        }
    }
    #endregion
}
