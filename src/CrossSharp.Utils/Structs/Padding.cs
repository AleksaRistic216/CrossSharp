namespace CrossSharp.Utils.Structs;

public struct Padding(int left = 0, int top = 0, int right = 0, int bottom = 0)
{
    public int Top { get; set; } = top;
    public int Bottom { get; set; } = bottom;
    public int Left { get; set; } = left;
    public int Right { get; set; } = right;

    public Padding(int all)
        : this(all, all, all, all) { }

    public Padding(int horizontal, int vertical)
        : this(horizontal, vertical, horizontal, vertical) { }

    public static Padding Zero => new(0);

    public int Horizontal => Left + Right;
    public int Vertical => Top + Bottom;
}
