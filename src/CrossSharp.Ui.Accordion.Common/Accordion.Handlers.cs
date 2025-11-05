namespace CrossSharp.Ui.Common;

partial class Accordion
{
    public EventHandler? OrientationChanged { get; set; }

    void RaiseOrientationChanged() => OrientationChanged?.Invoke(this, EventArgs.Empty);

    void OnOrientationChanged() => RaiseOrientationChanged();
}
