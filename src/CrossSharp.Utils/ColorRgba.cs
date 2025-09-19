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
    public static readonly ColorRgba White = new(1, 1, 1, 1);
    public static readonly ColorRgba Black = new(0, 0, 0, 1);
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

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
