using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IImagePreview : IControl, IDockable, IRoundedCorners, IBackgroundColorProvider
{
    IEfficientImage? Image { get; set; }
    EventHandler? OnImageChanged { get; set; }
    int RenderResolution { get; set; }
    ImagePreviewRenderMode RenderMode { get; set; }
}
