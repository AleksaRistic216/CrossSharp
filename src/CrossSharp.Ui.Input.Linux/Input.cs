using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
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
            ShiftCaretPosition(-1);
            return;
        }
        if (e.KeyCode == KeyCode.VcEnter && MultiLine)
        {
            Text += Environment.NewLine;
            _caretPosition.Y++;
            return;
        }
        if (e.Char is null)
            return;
        Text += e.Char;
        ShiftCaretPosition(1);
    }

    void ShiftCaretPosition(int amount)
    {
        if (!MultiLine)
        {
            _caretPosition.X += amount;
            if (_caretPosition.X < 0)
                _caretPosition.X = 0;
            if (_caretPosition.X > Text.Length)
                _caretPosition.X = Text.Length;
            return;
        }

        var lines = Text.Split(Environment.NewLine);
        _caretPosition.X += amount;
        while (_caretPosition is { X: < 0, Y: > 0 })
        {
            _caretPosition.Y--;
            _caretPosition.X += lines[_caretPosition.Y].Length;
        }
        if (_caretPosition.X < 0)
        {
            _caretPosition.X = 0;
        }
        else if (
            _caretPosition.Y < lines.Length - 1
            && _caretPosition.X == lines[_caretPosition.Y].Length
            && Text.EndsWith(Environment.NewLine)
        )
        {
            _caretPosition.Y++;
            _caretPosition.X = 0;
        }
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

    void InvalidateCaretBounds(IGraphics g)
    {
        // Call this within draw because graphics gets fcked up when created here. Should fix it though
        var form = this.GetForm() as IFormSDL;
        if (form is null)
            return;
        var text = MultiLine ? Text.Split(Environment.NewLine)[_caretPosition.Y] : Text;
        text = text[..Math.Min(_caretPosition.X, text.Length)];
        var textSize = g.MeasureText(text, FontFamily.Default, FontSize);
        var caretX = _lineGap + textSize.Width + CornerRadius / 2 + BorderWidth;
        var caretY = _lineGap / 2 + _caretPosition.Y * LineHeight + BorderWidth;
        _caretBounds = new Rectangle(caretX, caretY, 2, LineHeight);
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

    void DrawCaret(ref IGraphics g)
    {
        if (!IsFocused)
            return;
        InvalidateCaretBounds(g);
        var now = DateTime.Now;
        if ((now - _lastCaretStateUpdate).TotalMilliseconds >= 500)
        {
            _caretVisible = !_caretVisible;
            _lastCaretStateUpdate = now;
        }
        if (!_caretVisible)
            return;
        g.FillRectangle(_caretBounds.X, _caretBounds.Y, 2, LineHeight, ColorRgba.Black);
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
