using CrossSharp.Utils.Input;

namespace CrossSharp.Utils.Interfaces;

public interface IInputHandler
{
    event EventHandler<object>? KeyPressed;
    event EventHandler<MouseInputArgs>? MousePressed;
    event EventHandler<MouseInputArgs>? MouseReleased;
    event EventHandler<MouseInputArgs>? MouseMoved;
    event EventHandler<MouseInputArgs>? MouseWheel;
    event EventHandler<MouseInputArgs>? MouseDragged;
}
