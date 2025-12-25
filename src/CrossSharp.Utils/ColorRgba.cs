using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils;

/// <summary>
/// Color represented in RGBA format with float components ranging from 0.0 to 1.0
/// You can use ColorRgba.FromBytes(byte r, byte g, byte b, byte a) to create from 0-255 byte values
/// </summary>
/// <param name="r"></param>
/// <param name="g"></param>
/// <param name="b"></param>
/// <param name="a"></param>
public class ColorRgba(float r, float g, float b, float a)
{
    // Transparent and Basic
    public static readonly ColorRgba Transparent = new(0, 0, 0, 0);
    public static readonly ColorRgba Black = new(0, 0, 0, 1);
    public static readonly ColorRgba White = new(1, 1, 1, 1);

    // Grays
    public static readonly ColorRgba ReallyDarkGray = new(0.1f, 0.1f, 0.1f, 1);
    public static readonly ColorRgba DarkGray = new(0.2f, 0.2f, 0.2f, 1);
    public static readonly ColorRgba DimGray = new(0.41f, 0.41f, 0.41f, 1);
    public static readonly ColorRgba Gray = new(0.5f, 0.5f, 0.5f, 1);
    public static readonly ColorRgba DarkSlateGray = new(0.18f, 0.31f, 0.31f, 1);
    public static readonly ColorRgba SlateGray = new(0.44f, 0.5f, 0.56f, 1);
    public static readonly ColorRgba LightSlateGray = new(0.47f, 0.53f, 0.6f, 1);
    public static readonly ColorRgba Silver = new(0.75f, 0.75f, 0.75f, 1);
    public static readonly ColorRgba LightGray = new(0.83f, 0.83f, 0.83f, 1);
    public static readonly ColorRgba Gainsboro = new(0.86f, 0.86f, 0.86f, 1);
    public static readonly ColorRgba WhiteSmoke = new(0.96f, 0.96f, 0.96f, 1);

    // Whites and Off-Whites
    public static readonly ColorRgba Snow = new(1, 0.98f, 0.98f, 1);
    public static readonly ColorRgba GhostWhite = new(0.97f, 0.97f, 1, 1);
    public static readonly ColorRgba FloralWhite = new(1, 0.98f, 0.94f, 1);
    public static readonly ColorRgba Linen = new(0.98f, 0.94f, 0.9f, 1);
    public static readonly ColorRgba AntiqueWhite = new(0.98f, 0.92f, 0.84f, 1);
    public static readonly ColorRgba OldLace = new(0.99f, 0.96f, 0.9f, 1);
    public static readonly ColorRgba Ivory = new(1, 1, 0.94f, 1);
    public static readonly ColorRgba Beige = new(0.96f, 0.96f, 0.86f, 1);
    public static readonly ColorRgba Seashell = new(1, 0.96f, 0.93f, 1);
    public static readonly ColorRgba Honeydew = new(0.94f, 1, 0.94f, 1);
    public static readonly ColorRgba MintCream = new(0.96f, 1, 0.98f, 1);
    public static readonly ColorRgba Azure = new(0.94f, 1, 1, 1);
    public static readonly ColorRgba AliceBlue = new(0.94f, 0.97f, 1, 1);
    public static readonly ColorRgba LavenderBlush = new(1, 0.94f, 0.96f, 1);
    public static readonly ColorRgba Cornsilk = new(1, 0.97f, 0.86f, 1);

    // Reds
    public static readonly ColorRgba Red = new(1, 0, 0, 1);
    public static readonly ColorRgba DarkRed = new(0.55f, 0, 0, 1);
    public static readonly ColorRgba Maroon = new(0.5f, 0, 0, 1);
    public static readonly ColorRgba FireBrick = new(0.7f, 0.13f, 0.13f, 1);
    public static readonly ColorRgba Crimson = new(0.86f, 0.08f, 0.24f, 1);
    public static readonly ColorRgba IndianRed = new(0.8f, 0.36f, 0.36f, 1);
    public static readonly ColorRgba LightCoral = new(0.94f, 0.5f, 0.5f, 1);
    public static readonly ColorRgba Salmon = new(0.98f, 0.5f, 0.45f, 1);
    public static readonly ColorRgba DarkSalmon = new(0.91f, 0.59f, 0.48f, 1);
    public static readonly ColorRgba LightSalmon = new(1, 0.63f, 0.48f, 1);
    public static readonly ColorRgba MistyRose = new(1, 0.89f, 0.88f, 1);

    // Oranges
    public static readonly ColorRgba Orange = new(1, 0.65f, 0, 1);
    public static readonly ColorRgba DarkOrange = new(1, 0.55f, 0, 1);
    public static readonly ColorRgba OrangeRed = new(1, 0.27f, 0, 1);
    public static readonly ColorRgba Tomato = new(1, 0.39f, 0.28f, 1);
    public static readonly ColorRgba Coral = new(1, 0.5f, 0.31f, 1);

    // Yellows
    public static readonly ColorRgba Yellow = new(1, 1, 0, 1);
    public static readonly ColorRgba LightYellow = new(1, 1, 0.88f, 1);
    public static readonly ColorRgba LemonChiffon = new(1, 0.98f, 0.8f, 1);
    public static readonly ColorRgba PapayaWhip = new(1, 0.94f, 0.84f, 1);
    public static readonly ColorRgba Moccasin = new(1, 0.89f, 0.71f, 1);
    public static readonly ColorRgba PeachPuff = new(1, 0.85f, 0.73f, 1);
    public static readonly ColorRgba Gold = new(1, 0.84f, 0, 1);
    public static readonly ColorRgba Khaki = new(0.94f, 0.9f, 0.55f, 1);
    public static readonly ColorRgba DarkKhaki = new(0.74f, 0.72f, 0.42f, 1);

    // Browns
    public static readonly ColorRgba Brown = new(0.65f, 0.16f, 0.16f, 1);
    public static readonly ColorRgba SaddleBrown = new(0.55f, 0.27f, 0.07f, 1);
    public static readonly ColorRgba Sienna = new(0.63f, 0.32f, 0.18f, 1);
    public static readonly ColorRgba Chocolate = new(0.82f, 0.41f, 0.12f, 1);
    public static readonly ColorRgba Peru = new(0.8f, 0.52f, 0.25f, 1);
    public static readonly ColorRgba SandyBrown = new(0.96f, 0.64f, 0.38f, 1);
    public static readonly ColorRgba BurlyWood = new(0.87f, 0.72f, 0.53f, 1);
    public static readonly ColorRgba Tan = new(0.82f, 0.71f, 0.55f, 1);
    public static readonly ColorRgba RosyBrown = new(0.74f, 0.56f, 0.56f, 1);
    public static readonly ColorRgba Wheat = new(0.96f, 0.87f, 0.7f, 1);
    public static readonly ColorRgba NavajoWhite = new(1, 0.87f, 0.68f, 1);
    public static readonly ColorRgba Bisque = new(1, 0.89f, 0.77f, 1);
    public static readonly ColorRgba BlanchedAlmond = new(1, 0.92f, 0.8f, 1);

    // Greens
    public static readonly ColorRgba Green = new(0, 0.5f, 0, 1);
    public static readonly ColorRgba Lime = new(0, 1, 0, 1);
    public static readonly ColorRgba DarkGreen = new(0, 0.39f, 0, 1);
    public static readonly ColorRgba ForestGreen = new(0.13f, 0.55f, 0.13f, 1);
    public static readonly ColorRgba SeaGreen = new(0.18f, 0.55f, 0.34f, 1);
    public static readonly ColorRgba MediumSeaGreen = new(0.24f, 0.7f, 0.44f, 1);
    public static readonly ColorRgba LimeGreen = new(0.2f, 0.8f, 0.2f, 1);
    public static readonly ColorRgba LightGreen = new(0.56f, 0.93f, 0.56f, 1);
    public static readonly ColorRgba PaleGreen = new(0.6f, 0.98f, 0.6f, 1);
    public static readonly ColorRgba SpringGreen = new(0, 1, 0.5f, 1);
    public static readonly ColorRgba MediumSpringGreen = new(0, 0.98f, 0.6f, 1);
    public static readonly ColorRgba LawnGreen = new(0.49f, 0.99f, 0, 1);
    public static readonly ColorRgba Chartreuse = new(0.5f, 1, 0, 1);
    public static readonly ColorRgba GreenYellow = new(0.68f, 1, 0.18f, 1);
    public static readonly ColorRgba YellowGreen = new(0.6f, 0.8f, 0.2f, 1);
    public static readonly ColorRgba OliveDrab = new(0.42f, 0.56f, 0.14f, 1);
    public static readonly ColorRgba Olive = new(0.5f, 0.5f, 0, 1);
    public static readonly ColorRgba DarkOliveGreen = new(0.33f, 0.42f, 0.18f, 1);

    // Cyans and Teals
    public static readonly ColorRgba Cyan = new(0, 1, 1, 1);
    public static readonly ColorRgba Aqua = new(0, 1, 1, 1);
    public static readonly ColorRgba LightCyan = new(0.88f, 1, 1, 1);
    public static readonly ColorRgba PaleTurquoise = new(0.69f, 0.93f, 0.93f, 1);
    public static readonly ColorRgba Aquamarine = new(0.5f, 1, 0.83f, 1);
    public static readonly ColorRgba Turquoise = new(0.25f, 0.88f, 0.82f, 1);
    public static readonly ColorRgba MediumTurquoise = new(0.28f, 0.82f, 0.8f, 1);
    public static readonly ColorRgba DarkTurquoise = new(0, 0.81f, 0.82f, 1);
    public static readonly ColorRgba Teal = new(0, 0.5f, 0.5f, 1);
    public static readonly ColorRgba DarkCyan = new(0, 0.55f, 0.55f, 1);
    public static readonly ColorRgba CadetBlue = new(0.37f, 0.62f, 0.63f, 1);

    // Blues
    public static readonly ColorRgba Blue = new(0, 0, 1, 1);
    public static readonly ColorRgba Navy = new(0, 0, 0.5f, 1);
    public static readonly ColorRgba DarkBlue = new(0, 0, 0.55f, 1);
    public static readonly ColorRgba MediumBlue = new(0, 0, 0.8f, 1);
    public static readonly ColorRgba MidnightBlue = new(0.1f, 0.1f, 0.44f, 1);
    public static readonly ColorRgba RoyalBlue = new(0.25f, 0.41f, 0.88f, 1);
    public static readonly ColorRgba CornflowerBlue = new(0.39f, 0.58f, 0.93f, 1);
    public static readonly ColorRgba DodgerBlue = new(0.12f, 0.56f, 1, 1);
    public static readonly ColorRgba DeepSkyBlue = new(0, 0.75f, 1, 1);
    public static readonly ColorRgba LightSkyBlue = new(0.53f, 0.81f, 0.98f, 1);
    public static readonly ColorRgba SkyBlue = new(0.53f, 0.81f, 0.92f, 1);
    public static readonly ColorRgba SteelBlue = new(0.27f, 0.51f, 0.71f, 1);
    public static readonly ColorRgba LightSteelBlue = new(0.69f, 0.77f, 0.87f, 1);
    public static readonly ColorRgba LightBlue = new(0.68f, 0.85f, 0.9f, 1);
    public static readonly ColorRgba PowderBlue = new(0.69f, 0.88f, 0.9f, 1);

    // Purples and Violets
    public static readonly ColorRgba Purple = new(0.5f, 0, 0.5f, 1);
    public static readonly ColorRgba Indigo = new(0.29f, 0, 0.51f, 1);
    public static readonly ColorRgba DarkMagenta = new(0.55f, 0, 0.55f, 1);
    public static readonly ColorRgba DarkViolet = new(0.58f, 0, 0.83f, 1);
    public static readonly ColorRgba DarkSlateBlue = new(0.28f, 0.24f, 0.55f, 1);
    public static readonly ColorRgba SlateBlue = new(0.42f, 0.35f, 0.8f, 1);
    public static readonly ColorRgba MediumSlateBlue = new(0.48f, 0.41f, 0.93f, 1);
    public static readonly ColorRgba BlueViolet = new(0.54f, 0.17f, 0.89f, 1);
    public static readonly ColorRgba MediumPurple = new(0.58f, 0.44f, 0.86f, 1);
    public static readonly ColorRgba MediumOrchid = new(0.73f, 0.33f, 0.83f, 1);
    public static readonly ColorRgba Orchid = new(0.85f, 0.44f, 0.84f, 1);
    public static readonly ColorRgba Violet = new(0.93f, 0.51f, 0.93f, 1);
    public static readonly ColorRgba Plum = new(0.87f, 0.63f, 0.87f, 1);
    public static readonly ColorRgba Thistle = new(0.85f, 0.75f, 0.85f, 1);
    public static readonly ColorRgba Lavender = new(0.9f, 0.9f, 0.98f, 1);

    // Pinks and Magentas
    public static readonly ColorRgba Magenta = new(1, 0, 1, 1);
    public static readonly ColorRgba Fuchsia = new(1, 0, 1, 1);
    public static readonly ColorRgba DeepPink = new(1, 0.08f, 0.58f, 1);
    public static readonly ColorRgba HotPink = new(1, 0.41f, 0.71f, 1);
    public static readonly ColorRgba MediumVioletRed = new(0.78f, 0.08f, 0.52f, 1);
    public static readonly ColorRgba PaleVioletRed = new(0.86f, 0.44f, 0.58f, 1);
    public static readonly ColorRgba Pink = new(1, 0.75f, 0.8f, 1);
    public static readonly ColorRgba LightPink = new(1, 0.71f, 0.76f, 1);

    // Limitless Theme Colors
    public static readonly ColorRgba LimitlessPrimary = new(0x44 / 255f, 0x75 / 255f, 0xBA / 255f, 1.0f);
    public static readonly ColorRgba LimitlessSecondary = new(0xF9 / 255f, 0xF9 / 255f, 0xF9 / 255f, 1.0f);
    public static readonly ColorRgba LimitlessAccent = new(0xFF / 255f, 0xA7 / 255f, 0x26 / 255f, 1.0f);
    public static readonly ColorRgba LimitlessInput = new(0xEE / 255f, 0xEE / 255f, 0xEE / 255f, 1.0f);
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
