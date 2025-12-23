using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ImagePreview : ControlBase, IImagePreview
{
    IEfficientImage? _processedImage;
    Rectangle? _imageBounds;

    internal ImagePreview()
    {
        PerformTheme();
    }

    public sealed override void PerformTheme()
    {
        OnThemePerformed();
    }

    public override void DrawBackground(ref IGraphics g)
    {
        g.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    public override void Invalidate()
    {
        this.PerformDocking();
        ProcessImage();
        CalcImageBounds();
    }

    void ProcessImage()
    {
        if (Image == null)
        {
            _processedImage = null;
            return;
        }

        if (RenderResolution <= 0)
        {
            _processedImage = Image;
            return;
        }

        _processedImage = RenderMode switch
        {
            ImagePreviewRenderMode.Quality => Image,
            ImagePreviewRenderMode.Performance => Image.ScaledToFit(RenderResolution),
            ImagePreviewRenderMode.Default => Image,
            _ => Image,
        };
    }

    void CalcImageBounds()
    {
        if (_processedImage == null)
        {
            _imageBounds = null;
            return;
        }

        var imgSize = _processedImage.Size;
        if (imgSize.Width <= 0 || imgSize.Height <= 0)
        {
            _imageBounds = null;
            return;
        }

        float scaleX = (float)Width / imgSize.Width;
        float scaleY = (float)Height / imgSize.Height;
        float scale = Math.Min(scaleX, scaleY);

        int newWidth = (int)(imgSize.Width * scale);
        int newHeight = (int)(imgSize.Height * scale);
        int x = (Width - newWidth) / 2;
        int y = (Height - newHeight) / 2;

        _imageBounds = new Rectangle(x, y, newWidth, newHeight);
    }

    public override void DrawContent(ref IGraphics g)
    {
        if (_processedImage == null || !_imageBounds.HasValue)
            return;

        if (RenderMode == ImagePreviewRenderMode.Default && RenderResolution > 0)
        {
            var scaledImage = _processedImage.ScaledToFit(RenderResolution);
            g.DrawImage(scaledImage.Data, _imageBounds.Value);
        }
        else
        {
            g.DrawImage(_processedImage.Data, _imageBounds.Value);
        }
    }
}
