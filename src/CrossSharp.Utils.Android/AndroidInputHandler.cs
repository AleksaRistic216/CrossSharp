using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Android;

/// <summary>
/// Android implementation of IInputHandler.
/// On Android, input is handled through the View touch events rather than global hooks.
/// </summary>
public class AndroidInputHandler : IInputHandler
{
    public event EventHandler<KeyInputArgs>? KeyPressed;
    public event EventHandler<MouseInputArgs>? MousePressed;
    public event EventHandler<MouseInputArgs>? MouseReleased;
    public event EventHandler<MouseInputArgs>? MouseMoved;
    public event EventHandler<MouseWheelInputArgs>? MouseWheel;
    public event EventHandler<MouseInputArgs>? MouseDragged;

    public void StartListeningAsync(CancellationToken cancellationToken)
    {
        // No-op on Android - input is handled through View touch events
    }

    // Methods to raise events from CrossSharpView touch handling
    internal void RaiseMousePressed(MouseInputArgs args) => MousePressed?.Invoke(this, args);
    internal void RaiseMouseReleased(MouseInputArgs args) => MouseReleased?.Invoke(this, args);
    internal void RaiseMouseMoved(MouseInputArgs args) => MouseMoved?.Invoke(this, args);
    internal void RaiseMouseDragged(MouseInputArgs args) => MouseDragged?.Invoke(this, args);
}
