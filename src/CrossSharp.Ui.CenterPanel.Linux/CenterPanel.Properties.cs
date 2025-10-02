using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class CenterPanel
{
    ColorRgba _backgroundColor = Services.GetSingleton<ITheme>().BackgroundColor;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            RaiseBackgroundColorChanged();
        }
    }
    IControl? _child;
    public IControl? Child
    {
        get => _child;
        set
        {
            ValidateChild(value);
            if (_child == value)
                return;
            _child = value;
            RaiseChildChanged();
        }
    }
}
