using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Input;

public class MouseInputArgs : System.EventArgs
{
    public MouseButton Button { get; init; }
    public int Clicks { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
}
