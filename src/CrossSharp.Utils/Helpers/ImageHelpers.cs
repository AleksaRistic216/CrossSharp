using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SkiaSharp;

namespace CrossSharp.Utils.Helpers;

public static class ImageHelpers
{
    public static Image<Rgba32> FromSvgPath(string svgPath, int width, int height)
    {
        // Create Skia surface
        var info = new SKImageInfo(width, height);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        // Parse SVG path and draw
        var path = SKPath.ParseSvgPathData(svgPath);
        var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
        };

        // Optional: center and scale path to fit canvas
        var bounds = path.Bounds;
        var scaleX = width / bounds.Width;
        var scaleY = height / bounds.Height;
        var scale = Math.Min(scaleX, scaleY);
        var matrix = SKMatrix.CreateScale(scale, scale);
        matrix = matrix.PostConcat(
            SKMatrix.CreateTranslation(
                (width - bounds.Width * scale) / 2 - bounds.Left * scale,
                (height - bounds.Height * scale) / 2 - bounds.Top * scale
            )
        );
        path.Transform(matrix);

        canvas.DrawPath(path, paint);

        // Encode to PNG stream
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var ms = new MemoryStream(data.ToArray());

        // Load into ImageSharp
        return Image.Load<Rgba32>(ms);
    }
}
