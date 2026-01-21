using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class ImagePreviewFactory : IImagePreviewFactory
{
    public IImagePreview Create() => new ImagePreview();
}
