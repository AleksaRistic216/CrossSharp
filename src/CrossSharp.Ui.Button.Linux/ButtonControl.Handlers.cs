using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Linux;

public partial class Button
{
    public EventHandler? OnClick { get; set; }

    void SubscribeToInputs()
    {
        InputHandler.MousePressed += OnMousePressed;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver)
            return;
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    internal override void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        base.OnMouseMoved(sender, e);
        Redraw();
    }
}
