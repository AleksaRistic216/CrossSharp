using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Button : ControlBase, IButton
{
    public override void Initialize() { }

    public override void Invalidate()
    {
        InvalidateSize();
        InvalidateImage();
        CalcTextBounds();
        CalcImageBounds();
        if (BorderWidth == 0 && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderWidth = 2;
        if (BorderColor == ColorRgba.Transparent && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderColor = BackgroundColor;
    }

    void InvalidateSize()
    {
        if (!AutoSize)
            return;
        if (MaxWidth < MinWidth)
            throw new InvalidOperationException("MaxWidth cannot be less than MinWidth.");
        if (MaxHeight < MinHeight)
            throw new InvalidOperationException("MaxHeight cannot be less than MinHeight.");
        var textSize = GetTextSize();
        // Apply max constraints first
        Width = Math.Min(textSize.Width + Button._padding * 2, MaxWidth ?? int.MaxValue);
        Height = Math.Min(textSize.Height + Button._padding * 2, MaxHeight ?? int.MaxValue);
        // Then apply min constraints
        Width = Math.Max(Width, MinWidth ?? 0);
        Height = Math.Max(Height, MinHeight ?? 0);
    }

    void InvalidateImage()
    {
        int scaleToFitSize = Height < Width ? Height - Button._padding : Width - Button._padding;
        if (scaleToFitSize < 2)
            return;
        scaleToFitSize = (int)(
            scaleToFitSize * (Height < Width ? ImageScale.Height : ImageScale.Width)
        );
        if (scaleToFitSize < 2)
            return;
        _scaledToFitImage = Image?.ScaledToFit(scaleToFitSize);
    }

    Size GetTextSize()
    {
        if (this.GetForm() is not IFormSDL form)
            return Size.Empty;
        if (string.IsNullOrWhiteSpace(Text))
            return Size.Empty;
        using var graphics = new SDLGraphics(form.Renderer);
        return graphics.MeasureText(Text, _theme.DefaultFontFamily, _theme.DefaultFontSize);
    }

    void CalcTextBounds()
    {
        var textSize = GetTextSize();
        if (textSize == Size.Empty)
        {
            _textBounds = null;
            return;
        }

        var bounds = TextAlignment switch
        {
            Alignment.Center => new Rectangle(
                (Width - textSize.Width) / 2,
                (Height - textSize.Height) / 2,
                textSize.Width,
                textSize.Height
            ),
            Alignment.Left => new Rectangle(
                Button._padding,
                (Height - textSize.Height) / 2,
                textSize.Width,
                textSize.Height
            ),
            Alignment.Right => new Rectangle(
                Width - textSize.Width - Button._padding,
                (Height - textSize.Height) / 2,
                textSize.Width,
                textSize.Height
            ),
            _ => throw new ArgumentOutOfRangeException(),
        };
        if (
            _scaledToFitImage != null
            && ImagePlacement == ButtonImagePlacement.BeforeText
            && TextAlignment == Alignment.Left
        )
            bounds.X += _scaledToFitImage.Size.Width + Button._padding; // image width + padding
        if (
            _scaledToFitImage != null
            && ImagePlacement == ButtonImagePlacement.AfterText
            && TextAlignment == Alignment.Right
        )
            bounds.X -= _scaledToFitImage.Size.Width + Button._padding; // image width + padding
        _textBounds = bounds;
    }

    void CalcImageBounds()
    {
        // Ensure to call CalcTextBounds before this
        if (_scaledToFitImage == null)
            return;
        var imageY = (Height - _scaledToFitImage.Size.Height) / 2;
        var imageX = 0;
        if (_textBounds != null)
        {
            imageX = ImagePlacement switch
            {
                ButtonImagePlacement.BeforeText => _textBounds.Value.X
                    - _scaledToFitImage.Size.Width
                    - Button._padding,
                ButtonImagePlacement.AfterText => _textBounds.Value.Right + Button._padding,
                _ => (Width - _scaledToFitImage.Size.Width) / 2,
            };
        }
        else
        {
            imageX = TextAlignment switch
            {
                Alignment.Center => (Width - _scaledToFitImage.Size.Width) / 2,
                Alignment.Left => Button._padding,
                Alignment.Right => Width - _scaledToFitImage.Size.Width - Button._padding,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
        _imageBounds = new Rectangle(
            imageX,
            imageY,
            _scaledToFitImage.Size.Width,
            _scaledToFitImage.Size.Height
        );
    }

    public override void DrawContent(ref IGraphics g)
    {
        if (!string.IsNullOrEmpty(Text) && _textBounds.HasValue)
            g.DrawText(
                Text,
                _textBounds.Value.X,
                _textBounds.Value.Y,
                _theme.DefaultFontFamily,
                _theme.DefaultFontSize,
                Equals(ForegroundColor, ColorRgba.Transparent)
                    ? this.GetBackgroundColor().Contrasted
                    : ForegroundColor
            );
        if (Image != null && _imageBounds.HasValue)
            g.DrawImage(Image.Data, _imageBounds.Value);
    }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
    }
}
