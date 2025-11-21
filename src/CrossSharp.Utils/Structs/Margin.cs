namespace CrossSharp.Utils.Structs;

public struct Margin
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public int Horizontal
    {
        get => Left + Right;
        set
        {
            Left = value / 2;
            Right = value - Left;
        }
    }
    public int Vertical
    {
        get => Top + Bottom;
        set
        {
            Top = value / 2;
            Bottom = value - Top;
        }
    }

    public Margin(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Margin(int all)
    {
        Left = all;
        Top = all;
        Right = all;
        Bottom = all;
    }

    public Margin(Margin other)
    {
        Left = other.Left;
        Top = other.Top;
        Right = other.Right;
        Bottom = other.Bottom;
    }

    public Margin(int horizontal, int vertical)
    {
        Left = horizontal;
        Right = horizontal;
        Top = vertical;
        Bottom = vertical;
    }

    public static Margin Zero => new(0);

    public Margin Clone()
    {
        return new Margin(Left, Top, Right, Bottom);
    }

    public override string ToString()
    {
        return $"Margin(Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom})";
    }

    public Margin Add(Margin other)
    {
        return new Margin(Left + other.Left, Top + other.Top, Right + other.Right, Bottom + other.Bottom);
    }

    public Margin Subtract(Margin other)
    {
        return new Margin(Left - other.Left, Top - other.Top, Right - other.Right, Bottom - other.Bottom);
    }

    public static Margin operator +(Margin a, Margin b)
    {
        return a.Add(b);
    }

    public static Margin operator -(Margin a, Margin b)
    {
        return a.Subtract(b);
    }

    public static bool operator ==(Margin a, Margin b)
    {
        return a.Left == b.Left && a.Top == b.Top && a.Right == b.Right && a.Bottom == b.Bottom;
    }

    public static bool operator !=(Margin a, Margin b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Margin other)
            return false;

        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Top, Right, Bottom);
    }
}
