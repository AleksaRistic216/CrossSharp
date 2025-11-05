namespace CrossSharp.Ui.Common;

partial class Accordion
{
    public EventHandler? StateChanged { get; set; }

    void RaiseStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);

    void OnStateChanged()
    {
        Invalidate();
        RaiseStateChanged();
    }

    public EventHandler? OrientationChanged { get; set; }

    void RaiseOrientationChanged() => OrientationChanged?.Invoke(this, EventArgs.Empty);

    void OnOrientationChanged() => RaiseOrientationChanged();
}
