using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using Point = System.Drawing.Point;

namespace CrossSharp.Ui.Linux;

partial class Button : ControlBase, IButton
{
    public override void Initialize() { }

    public override void Invalidate()
    {
        CalcTextLocation();
        if (BorderWidth == 0 && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderWidth = 2;
        if (BorderColor == ColorRgba.Transparent && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderColor = BackgroundColor;
    }

    void CalcTextLocation()
    {
        if (GetForm() is not IFormSDL form)
            return;
        using var graphics = new SDLGraphics(form.Renderer);
        var textSize = graphics.MeasureText(Text, _theme.DefaultFontFamily, _theme.DefaultFontSize);
        _textLocation = TextAlignment switch
        {
            Alignment.Center => new Point(
                (Width - textSize.Width) / 2,
                (Height - textSize.Height) / 2
            ),
            Alignment.Left => new Point(5, (Height - textSize.Height) / 2),
            Alignment.Right => new Point(
                Width - textSize.Width - 5,
                (Height - textSize.Height) / 2
            ),
        };
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

        // var imagePath = Path.Combine(
        //     Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        //     "Downloads",
        //     "happy.png"
        // );
        // Services.GetSingleton<IEfficientImagesCache>().AddImage("happy", imagePath, true);
        // g.DrawImage(EfficientImage.Get("happy").ImageData, new Rectangle(0, 0, 32, 32));
    }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
    }
}
