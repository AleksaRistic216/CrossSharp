using CrossSharp.Utils.Input;

namespace CrossSharp.Utils.Interfaces;

public interface IInputHandler
{
    event EventHandler<InputArgs>? KeyPressed;
    event EventHandler<InputArgs>? MousePressed;
    event EventHandler<MouseMoveInputArgs>? MouseMoved;
    event EventHandler<InputArgs>? MouseWheel;
}
