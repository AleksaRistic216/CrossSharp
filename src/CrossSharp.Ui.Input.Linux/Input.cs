using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
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

    void InputHandlerOnKeyPressed(object? sender, KeyInputArgs e)
    {
        if (!IsFocused)
            return;
        if (e.KeyCode == KeyCode.VcBackspace)
        {
            if (Text.Length > 0)
                Text = Text[..^1];
            return;
        }
        if (e.Char is null)
            return;
        Text += e.Char;
    }

    public override void Initialize() { }

    public override void Invalidate()
    {
        CalcFontSize();
    }

    void CalcFontSize()
    {
        FontSize = Height - 8;
    }

    public override void Redraw() { }

    public override void DrawContent(ref IGraphics g)
    {
        DrawCaret(ref g);
        DrawText(ref g);
    }

    void DrawCaret(ref IGraphics g)
    {
        if (!IsFocused)
            return;
        var now = DateTime.Now;
        if ((now - _lastCaretStateUpdate).TotalMilliseconds >= 500)
        {
            _caretVisible = !_caretVisible;
            _lastCaretStateUpdate = now;
            Invalidate();
        }
        if (!_caretVisible)
            return;

        var textSize = g.MeasureText(Text, FontFamily.Default, FontSize);
        var caretX = _caretGap + textSize.Width;
        g.FillRectangle(caretX, _caretGap / 2, 2, Height - _caretGap, ColorRgba.Black);
    }

    void DrawText(ref IGraphics g)
    {
        var padding = 2;
        g.DrawText(Text, padding, padding, FontFamily.Default, FontSize, ColorRgba.Black);
    }
}
