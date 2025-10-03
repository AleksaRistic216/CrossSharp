using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public static class StackedLayoutExtensions
{
    public static void SetStackedLayoutItemSizing(this IControl control, ControlSizing sizing)
    {
        if (control.Parent is not IStackedLayout parent)
            throw new Exception($"Control is not child of {nameof(StackedLayout)}");

        parent.SetItemSizing(control, sizing);
    }
}
