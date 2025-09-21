using System.Drawing;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Linux;

public partial class Button
{
    public EventHandler? OnClick { get; set; }
    public EventHandler? OnTextChanged { get; set; }

    void RaiseTextChanged()
    {
        Invalidate();
        Redraw();
        OnTextChanged?.Invoke(this, EventArgs.Empty);
    }

    void RaiseClick()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    void SubscribeToInputs()
    {
        InputHandler.MousePressed += OnMousePressed;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver)
            return;
        RaiseClick();
    }

    internal override void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        base.OnMouseMoved(sender, e);
        Redraw();
    }

    protected override void RaiseOnSizeChanged(Size newSize)
    {
        base.RaiseOnSizeChanged(newSize);
        Invalidate();
    }

    protected override void RaiseOnLocationChanged(Point newLocation)
    {
        base.RaiseOnLocationChanged(newLocation);
        Invalidate();
    }
}
