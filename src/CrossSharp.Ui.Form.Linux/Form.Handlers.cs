using System.Drawing;
using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

partial class Form
{
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public EventHandler<ColorRgba>? OnBackgroundColorChanged { get; set; }
    public EventHandler? OnShow { get; set; }
    public EventHandler? OnClose { get; set; }

    void RaiseOnShow()
    {
        OnShow?.Invoke(this, EventArgs.Empty);
    }

    void RaiseOnClose()
    {
        OnClose?.Invoke(this, EventArgs.Empty);
    }

    void RaiseOnLocationChanged(Point newLocation)
    {
        OnLocationChanged?.Invoke(this, newLocation);
    }

    void RaiseOnSizeChanged(Size newSize)
    {
        OnSizeChanged?.Invoke(this, newSize);
    }

    void RaiseOnBackgroundColorChanged(ColorRgba newColor)
    {
        OnBackgroundColorChanged?.Invoke(this, newColor);
        Controls.BackgroundColor = newColor;
    }
}
