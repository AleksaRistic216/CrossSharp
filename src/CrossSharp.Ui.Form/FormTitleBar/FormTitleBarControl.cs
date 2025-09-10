using System.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl : ITitleBar
{
    public FormTitleBarControl(
        ITitleBarProvider titleBarProvider,
        IntPtr container,
        ISizeProvider sizeProvider
    )
    {
        _titleBarProvider = titleBarProvider;
        _panelControl = new PanelControl
        {
            ParentHandle = container,
            BackgroundColor = Color.Gray,
            Width = sizeProvider.Width,
            Height = _height,
        };
        _panelControl.Initialize();
        sizeProvider.OnSizeChanged += (s, e) =>
        {
            _panelControl.Width = e.Width;
        };
        Invalidate();
    }

    public void Show()
    {
        _panelControl.Show();
    }

    void Invalidate()
    {
        GtkHelpers.gtk_window_set_decorated(_titleBarProvider.Handle, false);
    }
}
