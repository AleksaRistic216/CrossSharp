using CrossSharp.Utils.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace CrossSharp.Utils.Interfaces;

public interface IGraphics : IDisposable
{
    void DrawImage(Image<Rgba32> image, Rectangle rect);
    void ForceRender();
    void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        float borderWidth
    );
    void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor);
    void DrawText(
        string text,
        int x,
        int y,
        FontFamily fontFamily,
        int fontSize,
        ColorRgba textColor
    );
    Size MeasureText(string text, FontFamily fontFamily, int fontSize);
    void SetClip(Rectangle rectangle);
    void ResetOffset();
    void SetOffset(int x, int y);
    void SetClip(Rectangle rectangle, int roundedCornersRadius);
}
