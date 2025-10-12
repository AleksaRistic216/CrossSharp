using System.Net.Mime;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace CrossSharp.Ui.Linux;

partial class Button : ControlBase, IButton
{
    public override void Initialize() { }

    public override void Invalidate()
    {
        if (GetForm() is not IFormSDL form)
            return;
        using var graphics = new SDLGraphics(form.Renderer);
        var textSize = graphics.MeasureText(Text, _theme.DefaultFontFamily, _theme.DefaultFontSize);
        _textLocation = new Point((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
        if (BorderWidth == 0 && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderWidth = 2;
        if (BorderColor == ColorRgba.Transparent && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderColor = BackgroundColor;
    }

    public override void DrawContent(ref IGraphics g)
    {
        g.DrawText(
            Text,
            _textLocation.X,
            _textLocation.Y,
            _theme.DefaultFontFamily,
            _theme.DefaultFontSize,
            ForegroundColor == ColorRgba.Transparent
                ? this.GetThemedBackgroundColor().Contrasted
                : ForegroundColor
        );

        var imagePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads",
            "happy.png"
        );
        // var image = Image.Load<Rgba32>(imagePath);
        // g.DrawImage(image, new Rectangle(0, 0, 32, 32));
    }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
    }
}
