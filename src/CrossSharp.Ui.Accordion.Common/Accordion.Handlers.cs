using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion
{
    public EventHandler? StateChanged { get; set; }

    void RaiseStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);

    void OnStateChanged()
    {
        Invalidate();
        RaiseStateChanged();
        if (Dock != DockStyle.None)
            this.GetForm()?.Invalidate();
    }

    public EventHandler? OrientationChanged { get; set; }

    void RaiseOrientationChanged() => OrientationChanged?.Invoke(this, EventArgs.Empty);

    void OnOrientationChanged() => RaiseOrientationChanged();
}
