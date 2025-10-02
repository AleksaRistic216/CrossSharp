using System.Drawing;
using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

partial class Form
{
    public EventHandler? OnUseNativeTitleBarChanged { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public EventHandler<ColorRgba>? OnBackgroundColorChanged { get; set; }
    public EventHandler? OnShow { get; set; }
    public EventHandler? OnClose { get; set; }

    void RaiseOnShow()
    {
        PerformLocation();
        OnShow?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void RaiseOnClose()
    {
        OnClose?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void RaiseOnLocationChanged(Point newLocation)
    {
        OnLocationChanged?.Invoke(this, newLocation);
    }

    protected virtual void RaiseOnSizeChanged(Size newSize)
    {
        Invalidate();
        OnSizeChanged?.Invoke(this, newSize);
    }

    protected virtual void RaiseOnBackgroundColorChanged(ColorRgba newColor)
    {
        OnBackgroundColorChanged?.Invoke(this, newColor);
        Controls.BackgroundColor = newColor;
    }

    protected virtual void RaiseOnNativeTitleBarChanged()
    {
        Invalidate();
        OnUseNativeTitleBarChanged?.Invoke(this, EventArgs.Empty);
    }
}
