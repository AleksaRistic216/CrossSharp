using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Panel() : CrossControl<IPanel>(Services.GetSingleton<IPanelFactory>().Create()), IPanel
{
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? OnBackgroundColorChange
    {
        get => Implementation.OnBackgroundColorChange;
        set => Implementation.OnBackgroundColorChange = value;
    }
    public ColorRgba ForegroundColor
    {
        get => Implementation.ForegroundColor;
        set => Implementation.ForegroundColor = value;
    }
}
