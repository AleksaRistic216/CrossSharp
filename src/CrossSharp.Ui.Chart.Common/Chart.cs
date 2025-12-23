using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Chart : ControlBase, IChart
{
    internal Chart() { }

    public override void PerformTheme() { }

    public override void Invalidate()
    {
        this.PerformDocking();
    }
}
