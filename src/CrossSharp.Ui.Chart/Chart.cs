using CrossSharp.Utils;
using CrossSharp.Utils.Attributes;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

[Control("Chart")]
public class Chart() : CrossControl<IChart>(Services.GetSingleton<IChartFactory>().Create()), IChart
{
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }
    public int DockIndex
    {
        get => Implementation.DockIndex;
        set => Implementation.DockIndex = value;
    }
    public DockStyle Dock
    {
        get => Implementation.Dock;
        set => Implementation.Dock = value;
    }
}
