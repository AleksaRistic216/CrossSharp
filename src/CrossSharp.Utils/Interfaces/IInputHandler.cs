using CrossSharp.Utils.Input;

namespace CrossSharp.Utils.Interfaces;

public interface IInputHandler
{
    void StartListeningAsync(CancellationToken cancellationToken);
    event EventHandler<KeyInputArgs>? KeyPressed;
    event EventHandler<MouseInputArgs>? MousePressed;
    event EventHandler<MouseInputArgs>? MouseReleased;
    event EventHandler<MouseInputArgs>? MouseMoved;
    event EventHandler<MouseWheelInputArgs>? MouseWheel;
    event EventHandler<MouseInputArgs>? MouseDragged;
}
