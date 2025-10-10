using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Label() : CrossControl<ILabel>(Services.GetSingleton<ILabelFactory>().Create()), ILabel
{
    public ColorRgba ForegroundColor
    {
        get => Implementation.ForegroundColor;
        set => Implementation.ForegroundColor = value;
    }
    public EventHandler<EventArgs>? OnTextChanged
    {
        get => Implementation.OnTextChanged;
        set => Implementation.OnTextChanged = value;
    }
    public string Text
    {
        get => Implementation.Text;
        set => Implementation.Text = value;
    }
    public string FontFamily
    {
        get => Implementation.FontFamily;
        set => Implementation.FontFamily = value;
    }
    public int FontSize
    {
        get => Implementation.FontSize;
        set => Implementation.FontSize = value;
    }
}
