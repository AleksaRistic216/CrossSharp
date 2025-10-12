using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CrossSharp.Utils.Drawing;

public class EfficientImage : IEfficientImage
{
    public string Identifier { get; private init; } = null!;
    public Image<Rgba32> ImageData { get; private init; } = null!;
    public Size Size { get; private init; }

    EfficientImage() { }

    internal static EfficientImage FromImage(string identifier, Image<Rgba32> image)
    {
        return new EfficientImage
        {
            Identifier = identifier,
            ImageData = image,
            Size = new Size(image.Width, image.Height),
        };
    }

    public static IEfficientImage Get(string identifier) =>
        Services.GetSingleton<IEfficientImagesCache>().GetImage(identifier);
}
