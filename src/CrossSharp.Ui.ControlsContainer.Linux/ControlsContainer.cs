using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ControlsContainer : IControlsContainer
{
    IPanel _widget;
    ColorRgba _backgroundColor;
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    public object Parent { get; set; }

    public int Width
    {
        get => _widget.Width;
        set => _widget.Width = value;
    }
    public int Height
    {
        get => _widget.Height;
        set => _widget.Height = value;
    }
    public EventHandler<Size>? OnSizeChanged
    {
        get => _widget.OnSizeChanged;
        set => _widget.OnSizeChanged = value;
    }

    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            Show();
        }
    }
    public List<IControl> Items { get; } = [];

    public ControlsContainer(
        IntPtr parentHandle,
        ISizeProvider sizeProvider,
        IBackgroundColorProvider backgroundColorProvider
    )
    {
        Handle = GtkHelpers.gtk_fixed_new();
        ParentHandle = parentHandle;
        GtkHelpers.gtk_window_set_child(parentHandle, Handle);
        _widget = new Panel
        {
            ParentHandle = Handle,
            BackgroundColor = ColorRgba.Purple,
            Width = sizeProvider.Width,
            Height = sizeProvider.Height,
        };
        _widget.Initialize();
        // sizeProvider.OnSizeChanged += (s, e) =>
        // {
        //     _widget.Width = e.Width;
        //     _widget.Height = e.Height;
        // };
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
        foreach (IControl item in Items)
            item.Redraw();
    }

    public void PerformLayout()
    {
        _widget.SuspendLayout();
        _widget.Width = Width;
        _widget.Height = Height;
        _widget.ResumeLayout();
    }
}
