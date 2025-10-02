using CrossSharp.Utils.Gtk;

namespace CrossSharp.Utils;

public class ColorRgba
{
    public static readonly ColorRgba Transparent = new(0, 0, 0, 0);
    public static readonly ColorRgba Red = new(1, 0, 0, 1);
    public static readonly ColorRgba Green = new(0, 1, 0, 1);
    public static readonly ColorRgba Blue = new(0, 0, 1, 1);
    public static readonly ColorRgba Purple = new(0.5f, 0, 0.5f, 1);
    public static readonly ColorRgba Gray = new(0.5f, 0.5f, 0.5f, 1);
    public static readonly ColorRgba LightGray = new(0.8f, 0.8f, 0.8f, 1);
    public static readonly ColorRgba DarkGray = new(0.2f, 0.2f, 0.2f, 1);
    public static readonly ColorRgba ReallyDarkGray = new(0.1f, 0.1f, 0.1f, 1);
    public static readonly ColorRgba Yellow = new(1, 1, 0, 1);
    public static readonly ColorRgba Orange = new(1, 0.65f, 0, 1);
    public static readonly ColorRgba Pink = new(1, 0.75f, 0.8f, 1);
    public static readonly ColorRgba White = new(1, 1, 1, 1);
    public static readonly ColorRgba WhiteSmoke = new(0.9f, 0.9f, 0.9f, 1);
    public static readonly ColorRgba Black = new(0, 0, 0, 1);
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

    const float HIGHLIGHT_FACTOR = 0.5f;
    public ColorRgba Highlighted =>
        new(
            Math.Min(R + HIGHLIGHT_FACTOR, 1),
            Math.Min(G + HIGHLIGHT_FACTOR, 1),
            Math.Min(B + HIGHLIGHT_FACTOR, 1),
            A
        );

    public ColorRgba(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    internal GtkRgba ToGtkRgba()
    {
        return new GtkRgba
        {
            red = R,
            green = G,
            blue = B,
            alpha = A,
        };
    }
}
