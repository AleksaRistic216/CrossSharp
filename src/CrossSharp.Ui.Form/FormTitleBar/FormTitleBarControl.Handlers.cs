using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

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
        _mouseDownFormPosition = _titleBarProvider.Location;
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _mouseDownMousePosition = null;
        _mouseDownFormPosition = null;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        var formLocation = _titleBarProvider.Location;
        var bounds = new Rectangle(formLocation.X, formLocation.Y, _width, _height);
        IsMouseOver = bounds.Contains(e.X, e.Y);
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver || _mouseDownMousePosition is null || _mouseDownFormPosition is null)
            return;

        var deltaX = e.X - _mouseDownMousePosition.Value.X;
        var deltaY = e.Y - _mouseDownMousePosition.Value.Y;
        var newLocation = new Point(
            _mouseDownFormPosition.Value.X + deltaX,
            _mouseDownFormPosition.Value.Y + deltaY
        );
        _titleBarProvider.Location = newLocation;
    }
}
