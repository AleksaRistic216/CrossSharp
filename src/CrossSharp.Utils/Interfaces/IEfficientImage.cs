using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CrossSharp.Utils.Interfaces;

public interface IEfficientImage
{
    string Identifier { get; }
    Image<Rgba32> ImageData { get; }
    Size Size { get; }
}
