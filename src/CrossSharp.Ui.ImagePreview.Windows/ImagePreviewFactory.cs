using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class ImagePreviewFactory : IImagePreviewFactory
{
    public IImagePreview Create()
    {
        var imagePreview = new ImagePreview();
        return imagePreview;
    }
}
