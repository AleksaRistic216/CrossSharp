namespace CrossSharp.Utils.Interfaces;

public interface IInputHandler
{
    event EventHandler<InputArgs>? KeyPressed;
    event EventHandler<InputArgs>? MousePressed;
    event EventHandler<InputArgs>? MouseMoved;
    event EventHandler<InputArgs>? MouseWheel;
}
