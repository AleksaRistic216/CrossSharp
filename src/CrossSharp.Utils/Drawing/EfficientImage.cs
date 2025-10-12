using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CrossSharp.Utils.Drawing;

public class EfficientImage : IEfficientImage
{
    public string Identifier { get; private init; } = null!;
    public Image<Rgba32> Data { get; private init; } = null!;
    public Size Size { get; private init; }

    Dictionary<int, IEfficientImage> _scaledToFitCache = new();

    EfficientImage() { }

    internal static EfficientImage FromImage(string identifier, Image<Rgba32> image)
    {
        return new EfficientImage
        {
            Identifier = identifier,
            Data = image,
            Size = new Size(image.Width, image.Height),
        };
    }

    public static IEfficientImage Get(string identifier) =>
        Services.GetSingleton<IEfficientImagesCache>().GetImage(identifier);

    /// <summary>
    /// Returns a new IEfficientImage scaled to fit within a square of the specified size,
    /// maintaining the aspect ratio. The larger side of the image will be equal to
    /// largerSidePixels, and the smaller side will be scaled proportionally.
    /// </summary>
    /// <param name="largerSidePixels"></param>
    /// <returns></returns>
    public IEfficientImage ScaledToFit(int largerSidePixels)
    {
        if (largerSidePixels <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(largerSidePixels),
                "Must be greater than zero."
            );
        if (_scaledToFitCache.TryGetValue(largerSidePixels, out var cachedImage))
            return cachedImage;

        int originalWidth = Data.Width;
        int originalHeight = Data.Height;

        if (originalWidth <= largerSidePixels && originalHeight <= largerSidePixels)
            return this; // No scaling needed

        float scale = Math.Min(
            (float)largerSidePixels / originalWidth,
            (float)largerSidePixels / originalHeight
        );
        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        var resizedImage = Data.Clone();
        resizedImage.Mutate(x => x.Resize(newWidth, newHeight));
        _scaledToFitCache[largerSidePixels] = FromImage(
            Identifier + $"_scaled_to_fit_{newWidth}x{newHeight}",
            resizedImage
        );
        return _scaledToFitCache[largerSidePixels];
    }
}
