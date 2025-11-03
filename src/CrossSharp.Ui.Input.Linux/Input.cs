using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Input : ControlBase, IInput
{
    public Input()
    {
        BorderColor = ColorRgba.Gray;
        BorderWidth = 1;
        InputHandler.KeyPressed += InputHandlerOnKeyPressed;
    }

    public override void Initialize() { }

    public override void Invalidate()
    {
        this.PerformDocking();
        CalcFontSize();
        InvalidatePlaceholderBounds();
        InvalidateContentBounds();
    }

    void InvalidateContentBounds()
    {
        _contentBounds = new Rectangle(
            _lineGap + CornerRadius / 2 + BorderWidth,
            _lineGap / 2 + BorderWidth,
            Width - CornerRadius - _lineGap * 2,
            Height - _lineGap - BorderWidth * 2
        );
    }

    void InvalidatePlaceholderBounds()
    {
        _placeholderBounds = new Rectangle(
            _lineGap + CornerRadius / 2 + BorderWidth,
            _lineGap / 2 + BorderWidth,
            Width - CornerRadius - _lineGap * 2,
            LineHeight
        );
    }

    void CalcFontSize()
    {
        // I do not like this at all
        if (MultiLine)
        {
            if (FontSize <= 0) // First time setup use calculation as singe line and breaks, so this fixes it back
                FontSize = Services.GetSingleton<ITheme>().DefaultFontSize;
            return;
        }

        // If single liner, override font size to fit height
        FontSize = Height - _lineGap * 2 - BorderWidth * 2;
    }

    public override void Redraw() { }

    public override void DrawContent(ref IGraphics g)
    {
        DrawPlaceholder(ref g);
        DrawText(ref g);
        DrawCaret(ref g);
    }

    void DrawPlaceholder(ref IGraphics g)
    {
        if (!string.IsNullOrEmpty(Text))
            return;
        if (string.IsNullOrEmpty(Placeholder))
            return;
        g.DrawText(
            Placeholder!,
            _placeholderBounds.X,
            _placeholderBounds.Y,
            FontFamily.Default,
            FontSize,
            ColorRgba.Gray
        );
    }

    void DrawText(ref IGraphics g)
    {
        var x = _contentBounds.X;
        var y = _contentBounds.Y;
        var clientBounds = this.GetClientBounds();
        foreach (var line in Text.Split(Environment.NewLine))
        {
            g.SetClip(
                clientBounds with
                {
                    Width = Width - CornerRadius,
                    Height = Height,
                },
                CornerRadius
            );
            g.DrawText(line, x, y, FontFamily.Default, FontSize, ColorRgba.Black);
            y += LineHeight;
        }
    }
}
