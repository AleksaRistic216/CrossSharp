using System.Drawing;

namespace CrossSharp.Utils.Drawing;

public class ClipState
{
    ClipState() { }

    public static ClipState Max =>
        new() { Bounds = Rectangle.FromLTRB(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue), CornerRadius = 0 };
    public static ClipState Empty => new() { Bounds = Rectangle.Empty, CornerRadius = 0 };
    public Rectangle Bounds { get; set; }
    public int CornerRadius { get; set; }

    /// <summary>
    /// Creates a new ClipState by intersecting the current state's bounds with the provided clipBounds and cornerRadius.
    /// </summary>
    /// <param name="currentState"></param>
    /// <param name="clipBounds"></param>
    /// <param name="cornerRadius"></param>
    /// <returns></returns>
    public static ClipState Create(ClipState currentState, Rectangle clipBounds, int cornerRadius)
    {
        return new ClipState
        {
            Bounds = Rectangle.Intersect(currentState.Bounds, clipBounds),
            CornerRadius = cornerRadius,
        };
    }
}
