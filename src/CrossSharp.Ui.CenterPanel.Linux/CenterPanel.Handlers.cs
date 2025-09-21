namespace CrossSharp.Ui.Linux;

public partial class CenterPanel
{
    EventHandler<EventArgs?> BackgroundColorChanged;
    EventHandler<EventArgs?> ChildChanged;

    void RaiseBackgroundColorChanged()
    {
        Redraw();
        BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
    }

    void RaiseChildChanged()
    {
        Invalidate();
        Redraw();
        ChildChanged?.Invoke(this, EventArgs.Empty);
    }
}
