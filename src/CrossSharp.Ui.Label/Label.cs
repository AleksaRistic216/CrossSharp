using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Label()
    : CrossWidget<ILabel>(ServicesPool.GetSingleton<ILabelFactory>().Create()),
        ILabel
{
    public EventHandler<EventArgs>? OnTextChanged
    {
        get => _impl.OnTextChanged;
        set => _impl.OnTextChanged = value;
    }
    public string Text
    {
        get => _impl.Text;
        set => _impl.Text = value;
    }
    public string FontFamily
    {
        get => _impl.FontFamily;
        set => _impl.FontFamily = value;
    }
    public int FontSize
    {
        get => _impl.FontSize;
        set => _impl.FontSize = value;
    }
}
