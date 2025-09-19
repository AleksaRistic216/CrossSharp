using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class ControlsContainer : IControlsContainer
{
    GtkWidget _widget;
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    public object Parent { get; set; }
    public List<IControl> Items { get; } = [];

    public ControlsContainer(IntPtr parentHandle, ISizeProvider sizeProvider)
    {
        Handle = GtkHelpers.gtk_fixed_new();
        ParentHandle = parentHandle;
        GtkHelpers.gtk_window_set_child(parentHandle, Handle);
        _widget = new PanelControl
        {
            ParentHandle = Handle,
            BackgroundColor = ColorRgba.Red,
            Width = sizeProvider.Width,
            Height = sizeProvider.Height,
        };
        _widget.Initialize();
        sizeProvider.OnSizeChanged += (s, e) =>
        {
            _widget.Width = e.Width;
            _widget.Height = e.Height;
        };
    }

    public virtual void Show()
    {
        GtkHelpers.gtk_widget_show(Handle);
        _widget.Show();
        foreach (IControl item in Items)
            item.Show();
    }

    public void Dispose()
    {
        foreach (IControl item in Items)
            item.Dispose();
    }

    public void Add(IControl control)
    {
        Items.Add(control);
        control.ParentHandle = Handle;
        control.Parent = this;
        control.Initialize();
    }

    public void Redraw()
    {
        // foreach (IControl item in Items.OrderBy(x => x.ZIndex))
        //     item.Show();
    }
}
