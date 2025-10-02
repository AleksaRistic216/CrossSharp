using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public sealed class WindowContainer : IWindowContainer
{
    GtkGrid _grid;
    readonly IPanel _widget;
    ColorRgba _backgroundColor = null!;
    public IntPtr Handle => _grid.Handle;
    public IntPtr ParentHandle { get; set; }
    object _parent = null!;
    public object Parent
    {
        get => _parent;
        set { _parent = value; }
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

    public WindowContainer(
        IntPtr parentHandle,
        ISizeProvider sizeProvider,
        IBackgroundColorProvider backgroundColorProvider
    )
    {
        _grid = new GtkGrid();
        ParentHandle = parentHandle;
        GtkHelpers.gtk_window_set_child(parentHandle, Handle);
        _widget = new Panel
        {
            Parent = this,
            ZIndex = -1,
            ParentHandle = Handle,
            BackgroundColor = backgroundColorProvider.BackgroundColor,
            BorderColor = ColorRgba.Yellow,
            Width = sizeProvider.Width,
            Height = sizeProvider.Height,
        };
        _widget.Initialize();
    }

    public void Show()
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

    public void Attach(IControl control)
    {
        Attach(control, 0, 1, 1, 1);
    }

    public void Attach(IControl control, int column, int row, int width, int height)
    {
        _grid.Attach(control.Handle, column, row, width, height);
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

    public IEnumerator GetEnumerator() => Items.GetEnumerator();

    public void CopyTo(Array array, int index) => Items.CopyTo((IControl[])array, index);

    public int Count => Items.Count;
    public bool IsSynchronized => false;
    public object SyncRoot => this;
}
