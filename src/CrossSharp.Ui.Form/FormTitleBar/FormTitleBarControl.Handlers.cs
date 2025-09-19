using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.X11;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl
{
    IInputHandler _inputHandler;
    public EventHandler? TypeChanged { get; set; }

    void RaiseTypeChanged()
    {
        Invalidate();
        TypeChanged?.Invoke(this, EventArgs.Empty);
    }

    void InitializeInputHandler()
    {
        _inputHandler = ServicesPool.GetSingleton<IInputHandler>();
    }

    void SubscribeToInputEvents()
    {
        _inputHandler.MousePressed += OnMousePressed;
        _inputHandler.MouseReleased += OnMouseReleased;
        _inputHandler.MouseMoved += OnMouseMoved;
        _inputHandler.MouseDragged += OnMouseDragged;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        _mouseDownMousePosition = new Point(e.X, e.Y);
        _mouseDownFormPosition = _form.Location;
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _mouseDownMousePosition = null;
        _mouseDownFormPosition = null;

        if (_form is not IForm form)
            return;
        if (form.WindowSurfaceHandle == IntPtr.Zero)
            return;
        uint x11Surface = GtkHelpers.gdk_x11_surface_get_xid(form.WindowSurfaceHandle);
        if (x11Surface == 0)
            return;
        IntPtr x11Display = GtkHelpers.gdk_x11_display_get_xdisplay(form.DisplayHandle);
        if (x11Display == IntPtr.Zero)
            return;

        X11Helpers.XGetWindowAttributes(x11Display, x11Surface, out XWindowAttributes attrs);
        form.Location = new Point(attrs.x, attrs.y);
        _lastDragTime = DateTime.Now;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        var formLocation = _form.Location;
        var bounds = new Rectangle(formLocation.X, formLocation.Y, _width, _height);
        IsMouseOver = bounds.Contains(e.X, e.Y);
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver || _mouseDownMousePosition is null || _mouseDownFormPosition is null)
            return;
        var dx = e.X - _mouseDownMousePosition.Value.X;
        var dy = e.Y - _mouseDownMousePosition.Value.Y;
        if (
            Math.Abs(dx - _deltaX) < MOVEMENT_TRESHOLD
            && Math.Abs(dy - _deltaY) < MOVEMENT_TRESHOLD
        )
            return;
        if ((DateTime.Now - _lastDragTime).TotalMilliseconds < (1000f / MOVEMENT_FPS))
            return;
        _lastDragTime = DateTime.Now;
        _deltaX = dx;
        _deltaY = dy;
        var newLocation = new Point(
            _mouseDownFormPosition.Value.X + _deltaX,
            _mouseDownFormPosition.Value.Y + _deltaY
        );
        _form.Location = newLocation;
    }
}
