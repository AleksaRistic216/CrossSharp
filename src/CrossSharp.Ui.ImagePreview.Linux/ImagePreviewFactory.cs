using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class ImagePreviewFactory : IImagePreviewFactory
{
    public IImagePreview Create()
    {
        var imagePreview = new ImagePreview();
        return imagePreview;
    }
}
