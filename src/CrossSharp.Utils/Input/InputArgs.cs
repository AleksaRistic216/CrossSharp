using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Input;

public abstract class InputArgs : EventArgs { }

public class MouseMoveInputArgs : InputArgs
{
    public MouseButton Button { get; init; }
    public int Clicks { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
}
