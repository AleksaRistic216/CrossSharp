using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

partial class ThemePickerDropdownItem
{
    public EventHandler? Click { get; set; }

    void RaiseClick()
    {
        Click?.Invoke(this, EventArgs.Empty);
    }

    void OnClick(object? sender, EventArgs e)
    {
        if (!MouseHelpers.IsEventForThisForm(this))
            return;
        if (!IsMouseOver)
            return;
        SelectTheme();
        MainThreadDispatcher.Invoke(RaiseClick);
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
        BackgroundColor = IsMouseOver ? _itemTheme.LayoutBackgroundColor.Highlighted : _itemTheme.LayoutBackgroundColor;
        _layout.BackgroundColor = BackgroundColor;
    }
}
