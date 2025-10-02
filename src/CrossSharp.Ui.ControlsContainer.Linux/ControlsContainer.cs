using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ControlsContainer : IControlsContainer
{
    readonly IPanel _widget;
    ColorRgba _backgroundColor;
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    object _parent;
    public object Parent
    {
        get => _parent;
        set
        {
            _parent = value;
            _widget.Parent = value;
        }
    }

    public int BorderWidth
    {
        get => _widget.BorderWidth;
        set => _widget.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => _widget.BorderColor;
        set => _widget.BorderColor = value;
    }

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
            BackgroundColor = backgroundColorProvider.BackgroundColor,
            BorderColor = ColorRgba.Yellow,
            Width = sizeProvider.Width,
            Height = sizeProvider.Height,
        };
        _widget.Initialize();
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

    public void LimitClip(ref Graphics g)
    {
        g.SetClip(
            new Rectangle(
                BorderWidth,
                BorderWidth,
                Width - BorderWidth * 2,
                Height - BorderWidth * 2
            )
        );
    }
}
