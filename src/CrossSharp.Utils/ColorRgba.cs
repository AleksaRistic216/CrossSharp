using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils;

public class ColorRgba(float r, float g, float b, float a)
{
    public static readonly ColorRgba FloralWhite = new(1.0f, 0.98f, 0.94f, 1.0f);
    public static readonly ColorRgba Linen = new(0.98f, 0.94f, 0.9f, 1.0f);
    public static readonly ColorRgba MistyRose = new(1.0f, 0.89f, 0.88f, 1.0f);
    public static readonly ColorRgba LightCoral = new(0.94f, 0.5f, 0.5f, 1.0f);
    public static readonly ColorRgba LimitlessPrimary = new(0x44 / 255f, 0x75 / 255f, 0xBA / 255f, 1.0f);
    public static readonly ColorRgba LimitlessSecondary = new(0xF9 / 255f, 0xF9 / 255f, 0xF9 / 255f, 1.0f);
    public static readonly ColorRgba LimitlessAccent = new(0xFF / 255f, 0xA7 / 255f, 0x26 / 255f, 1.0f);
    public static readonly ColorRgba LimitlessInput = new(0xEE / 255f, 0xEE / 255f, 0xEE / 255f, 1.0f);
    public static readonly ColorRgba Transparent = new(0, 0, 0, 0);
    public static readonly ColorRgba Red = new(1, 0, 0, 1);
    public static readonly ColorRgba Green = new(0, 1, 0, 1);
    public static readonly ColorRgba LightGreen = new(0.5f, 1, 0.5f, 1);
    public static readonly ColorRgba DarkGreen = new(0, 0.5f, 0, 1);
    public static readonly ColorRgba LightBlue = new(0.5f, 0.5f, 1, 1);
    public static readonly ColorRgba DodgerBlue = new(0.12f, 0.56f, 1, 1);
    public static readonly ColorRgba DeepSkyBlue = new(0, 0.75f, 1, 1);
    public static readonly ColorRgba SteelBlue = new(0.27f, 0.51f, 0.71f, 1);
    public static readonly ColorRgba DarkBlue = new(0, 0, 0.5f, 1);
    public static readonly ColorRgba Blue = new(0, 0, 1, 1);
    public static readonly ColorRgba Purple = new(0.5f, 0, 0.5f, 1);
    public static readonly ColorRgba Gray = new(0.5f, 0.5f, 0.5f, 1);
    public static readonly ColorRgba LightGray = new(0.8f, 0.8f, 0.8f, 1);
    public static readonly ColorRgba Silver = new(0.75f, 0.75f, 0.75f, 1);
    public static readonly ColorRgba Gainsboro = new(0.86f, 0.86f, 0.86f, 1);
    public static readonly ColorRgba DarkGray = new(0.2f, 0.2f, 0.2f, 1);
    public static readonly ColorRgba DimGray = new(0.3f, 0.3f, 0.3f, 1);
    public static readonly ColorRgba ReallyDarkGray = new(0.1f, 0.1f, 0.1f, 1);
    public static readonly ColorRgba Yellow = new(1, 1, 0, 1);
    public static readonly ColorRgba Cyan = new(0, 1, 1, 1);
    public static readonly ColorRgba Orange = new(1, 0.65f, 0, 1);
    public static readonly ColorRgba Pink = new(1, 0.75f, 0.8f, 1);
    public static readonly ColorRgba LightPink = new(1, 0.85f, 0.9f, 1);
    public static readonly ColorRgba HotPink = new(1, 0.4f, 0.7f, 1);
    public static readonly ColorRgba MediumVioletRed = new(0.78f, 0.08f, 0.52f, 1);
    public static readonly ColorRgba White = new(1, 1, 1, 1);
    public static readonly ColorRgba WhiteSmoke = new(0.9f, 0.9f, 0.9f, 1);
    public static readonly ColorRgba Black = new(0, 0, 0, 1);
    public static ColorRgba RandomColor =>
        new((float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble(), 1);
    public float R { get; } = r;
    public float G { get; } = g;
    public float B { get; } = b;
    public float A { get; } = a;
    public byte RByte => (byte)(R * 255);
    public byte GByte => (byte)(G * 255);
    public byte BByte => (byte)(B * 255);
    public byte AByte => (byte)(A * 255);

    const float HIGHLIGHT_FACTOR = 0.5f;
    public ColorRgba Highlighted =>
        new(Math.Min(R + HIGHLIGHT_FACTOR, 1), Math.Min(G + HIGHLIGHT_FACTOR, 1), Math.Min(B + HIGHLIGHT_FACTOR, 1), A);

    const float SELECTED_FACTOR = 0.3f;
    public ColorRgba Selected =>
        new(Math.Min(R + SELECTED_FACTOR, 1), Math.Min(G + SELECTED_FACTOR, 1), Math.Min(B + SELECTED_FACTOR, 1), A);

    const float DARKEN_FACTOR = 0.15f;
    public ColorRgba Darkened =>
        new(Math.Max(R - DARKEN_FACTOR, 0), Math.Max(G - DARKEN_FACTOR, 0), Math.Max(B - DARKEN_FACTOR, 0), A);

    const float DISABLED_FACTOR = 0.5f;
    public ColorRgba Disabled => new(R * DISABLED_FACTOR, G * DISABLED_FACTOR, B * DISABLED_FACTOR, A);

    public ColorRgba Contrasted =>
        A < 0.01f ? Black
        : (R + G + B) / 3 < 0.5f ? White
        : Black;
    public static ColorRgba Empty { get; } = new(0, 0, 0, 0);

    public static ColorRgba FromBytes(byte r, byte g, byte b, byte a) => new(r / 255f, g / 255f, b / 255f, a / 255f);

    public override string ToString() => $"RGBA({RByte}, {GByte}, {BByte}, {AByte})";

    // ReSharper disable once InconsistentNaming
    public SDLColor ToSDLColor() =>
        new()
        {
            r = RByte,
            g = GByte,
            b = BByte,
            a = AByte,
        };

    public override bool Equals(object? obj)
    {
        const float tolerance = 0.01f;
        if (obj is not ColorRgba other)
            return false;
        return Math.Abs(R - other.R) < tolerance
            && Math.Abs(G - other.G) < tolerance
            && Math.Abs(B - other.B) < tolerance
            && Math.Abs(A - other.A) < tolerance;
    }

    protected bool Equals(ColorRgba other) =>
        R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);

    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B, A);
    }
}
