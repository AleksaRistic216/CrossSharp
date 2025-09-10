using System.Drawing;

namespace CrossSharp.Ui.FormLinux;

partial class FormLinux
{
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
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
}
