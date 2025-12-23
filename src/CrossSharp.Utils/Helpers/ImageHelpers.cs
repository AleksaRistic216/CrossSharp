using System.Xml.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;

namespace CrossSharp.Utils.Helpers;

public static partial class ImageHelpers
{
    public static Image<Rgba32> FromSvg(string svgContent, int width, int height, SKColor? color = null)
    {
        var fillColor = color ?? SKColors.Black;

        var info = new SKImageInfo(width, height);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        var doc = XDocument.Parse(svgContent);
        var svg = doc.Root!;
        var ns = svg.GetDefaultNamespace();

        // Parse viewBox for scaling
        var viewBox = svg.Attribute("viewBox")?.Value;
        float viewBoxWidth = width;
        float viewBoxHeight = height;
        if (!string.IsNullOrEmpty(viewBox))
        {
            var parts = viewBox.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 4)
            {
                viewBoxWidth = float.Parse(parts[2]);
                viewBoxHeight = float.Parse(parts[3]);
            }
        }

        var scaleX = width / viewBoxWidth;
        var scaleY = height / viewBoxHeight;
        var scale = Math.Min(scaleX, scaleY);
        canvas.Scale(scale, scale);
        canvas.Translate((width / scale - viewBoxWidth) / 2, (height / scale - viewBoxHeight) / 2);

        // Parse stroke-width from root
        var defaultStrokeWidth = float.Parse(svg.Attribute("stroke-width")?.Value ?? "2");

        foreach (var element in svg.Descendants())
        {
            var strokeWidth = float.Parse(element.Attribute("stroke-width")?.Value ?? defaultStrokeWidth.ToString());
            var paint = new SKPaint
            {
                Color = fillColor,
                IsAntialias = true,
                StrokeWidth = strokeWidth,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
            };

            var fillAttr = element.Attribute("fill")?.Value;
            var strokeAttr = element.Attribute("stroke")?.Value ?? svg.Attribute("stroke")?.Value;

            if (strokeAttr == "currentColor" || !string.IsNullOrEmpty(strokeAttr))
            {
                paint.Style = SKPaintStyle.Stroke;
            }
            else if (fillAttr != "none")
            {
                paint.Style = SKPaintStyle.Fill;
            }

            switch (element.Name.LocalName)
            {
                case "path":
                    var d = element.Attribute("d")?.Value;
                    if (!string.IsNullOrEmpty(d))
                    {
                        var path = SKPath.ParseSvgPathData(d);
                        canvas.DrawPath(path, paint);
                    }
                    break;
                case "rect":
                    var x = float.Parse(element.Attribute("x")?.Value ?? "0");
                    var y = float.Parse(element.Attribute("y")?.Value ?? "0");
                    var w = float.Parse(element.Attribute("width")?.Value ?? "0");
                    var h = float.Parse(element.Attribute("height")?.Value ?? "0");
                    var rx = float.Parse(element.Attribute("rx")?.Value ?? "0");
                    var ry = float.Parse(element.Attribute("ry")?.Value ?? rx.ToString());
                    canvas.DrawRoundRect(new SKRoundRect(new SKRect(x, y, x + w, y + h), rx, ry), paint);
                    break;
                case "line":
                    var x1 = float.Parse(element.Attribute("x1")?.Value ?? "0");
                    var y1 = float.Parse(element.Attribute("y1")?.Value ?? "0");
                    var x2 = float.Parse(element.Attribute("x2")?.Value ?? "0");
                    var y2 = float.Parse(element.Attribute("y2")?.Value ?? "0");
                    canvas.DrawLine(x1, y1, x2, y2, paint);
                    break;
                case "circle":
                    var cx = float.Parse(element.Attribute("cx")?.Value ?? "0");
                    var cy = float.Parse(element.Attribute("cy")?.Value ?? "0");
                    var r = float.Parse(element.Attribute("r")?.Value ?? "0");
                    canvas.DrawCircle(cx, cy, r, paint);
                    break;
            }
        }

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var ms = new MemoryStream(data.ToArray());
        return Image.Load<Rgba32>(ms);
    }

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
