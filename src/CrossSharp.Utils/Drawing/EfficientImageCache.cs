using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CrossSharp.Utils.Drawing;

public class EfficientImageCache : IEfficientImagesCache
{
    const string ImageAlreadyExistsMessage =
        "An image with the identifier '{0}' already exists in the cache.";
    const string ImageNotFoundMessage =
        "No image with the identifier '{0}' was found in the cache.";
    readonly Dictionary<string, IEfficientImage> _cache = new();

    public void AddImage(string identifier, byte[] imageData, bool overwrite = false)
    {
        if (!overwrite && _cache.ContainsKey(identifier))
            throw new ArgumentException(string.Format(ImageAlreadyExistsMessage, identifier));
        Image.Load<Rgba32>(imageData);
        _cache[identifier] = EfficientImage.FromImage(identifier, Image.Load<Rgba32>(imageData));
    }

    public void AddImage(string identifier, string filePath, bool overwrite = false)
    {
        if (!overwrite && _cache.ContainsKey(identifier))
            throw new ArgumentException(string.Format(ImageAlreadyExistsMessage, identifier));
        _cache[identifier] = EfficientImage.FromImage(identifier, Image.Load<Rgba32>(filePath));
    }

    public void AddImage(string identifier, Image image, bool overwrite = false)
    {
        if (!overwrite && _cache.ContainsKey(identifier))
            throw new ArgumentException(string.Format(ImageAlreadyExistsMessage, identifier));
        _cache[identifier] = EfficientImage.FromImage(identifier, image.CloneAs<Rgba32>());
    }

    public IEfficientImage GetImage(string identifier)
    {
        if (_cache.TryGetValue(identifier, out var efficientImage))
            return efficientImage;
        throw new KeyNotFoundException(string.Format(ImageNotFoundMessage, identifier));
    }
}
