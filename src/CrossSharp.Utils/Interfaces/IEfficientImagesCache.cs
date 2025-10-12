using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CrossSharp.Utils.Interfaces;

public interface IEfficientImagesCache
{
    /// <summary>
    /// Adds an image to the cache with a unique identifier.
    /// If an image with the same identifier already exists, it will be overwritten if 'overwrite' is true otherwise an exception will be thrown.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="imageData"></param>
    /// <returns></returns>
    void AddImage(string identifier, byte[] imageData, bool overwrite = false);

    /// <summary>
    /// Adds an image to the cache with a unique identifier.
    /// If an image with the same identifier already exists, it will be overwritten if 'overwrite' is true otherwise an exception will be thrown.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="filePath"></param>
    /// <param name="overwrite"></param>
    /// <returns></returns>
    void AddImage(string identifier, string filePath, bool overwrite = false);

    /// <summary>
    /// Adds an image to the cache with a unique identifier.
    /// If an image with the same identifier already exists, it will be overwritten if 'overwrite' is true otherwise an exception will be thrown.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="image"></param>
    /// <param name="overwrite"></param>
    /// <returns></returns>
    void AddImage(string identifier, Image image, bool overwrite = false);

    /// <summary>
    /// Gets an image from the cache by its unique identifier.
    /// If the image does not exist, an exception will be thrown.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    IEfficientImage GetImage(string identifier);
}
