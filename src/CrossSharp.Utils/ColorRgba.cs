using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils;

public class ColorRgba
{
    public static readonly ColorRgba Transparent = new(0, 0, 0, 0);
    public static readonly ColorRgba Red = new(1, 0, 0, 1);
    public static readonly ColorRgba Green = new(0, 1, 0, 1);
    public static readonly ColorRgba LightGreen = new(0.5f, 1, 0.5f, 1);
    public static readonly ColorRgba DarkGreen = new(0, 0.5f, 0, 1);
    public static readonly ColorRgba LightBlue = new(0.5f, 0.5f, 1, 1);
    public static readonly ColorRgba DarkBlue = new(0, 0, 0.5f, 1);
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
    public static ColorRgba RandomColor =>
        new(
            (float)Random.Shared.NextDouble(),
            (float)Random.Shared.NextDouble(),
            (float)Random.Shared.NextDouble(),
            1
        );
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
    public byte RByte => (byte)(R * 255);
    public byte GByte => (byte)(G * 255);
    public byte BByte => (byte)(B * 255);
    public byte AByte => (byte)(A * 255);

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

    public override string ToString() => $"RGBA({RByte}, {GByte}, {BByte}, {AByte})";

    public SDLColor ToSDLColor() =>
        new()
        {
            r = RByte,
            g = GByte,
            b = BByte,
            a = AByte,
        };
}
