using CrossSharp.Utils;
using CrossSharp.Utils.Attributes;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

[Control("Image Preview", [ControlGroup.Display], ControlIcon.ImagePreview)]
public class ImagePreview()
    : CrossControl<IImagePreview>(Services.GetSingleton<IImagePreviewFactory>().Create()),
        IImagePreview
{
    public IEfficientImage? Image
    {
        get => Implementation.Image;
        set => Implementation.Image = value;
    }

    public EventHandler? OnImageChanged
    {
        get => Implementation.OnImageChanged;
        set => Implementation.OnImageChanged = value;
    }

    public int RenderResolution
    {
        get => Implementation.RenderResolution;
        set => Implementation.RenderResolution = value;
    }

    public ImagePreviewRenderMode RenderMode
    {
        get => Implementation.RenderMode;
        set => Implementation.RenderMode = value;
    }

    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }

    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }

    public int CornerRadius
    {
        get => Implementation.CornerRadius;
        set => Implementation.CornerRadius = value;
    }

    public int DockIndex
    {
        get => Implementation.DockIndex;
        set => Implementation.DockIndex = value;
    }

    public DockStyle Dock
    {
        get => Implementation.Dock;
        set => Implementation.Dock = value;
    }
}
