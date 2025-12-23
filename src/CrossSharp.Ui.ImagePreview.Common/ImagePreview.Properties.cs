using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ImagePreview
{
    IEfficientImage? _image;
    public IEfficientImage? Image
    {
        get => _image;
        set
        {
            if (Equals(_image, value))
                return;
            _image = value;
            OnImageChangedInternal();
        }
    }

    int _renderResolution = 400;
    public int RenderResolution
    {
        get => _renderResolution;
        set
        {
            if (_renderResolution == value)
                return;
            _renderResolution = value;
            Invalidate();
        }
    }

    ImagePreviewRenderMode _renderMode = ImagePreviewRenderMode.Default;
    public ImagePreviewRenderMode RenderMode
    {
        get => _renderMode;
        set
        {
            if (_renderMode == value)
                return;
            _renderMode = value;
            Invalidate();
        }
    }

    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            OnBackgroundColorChangedInternal();
        }
    }

    public int CornerRadius { get; set; }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
}
