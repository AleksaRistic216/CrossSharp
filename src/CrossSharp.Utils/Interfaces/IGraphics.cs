using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IGraphics : IDisposable
{
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
    void SetOffset(int locationX, int locationY);
    void SetClip(Rectangle rectangle, int roundedCornersRadius);
}
