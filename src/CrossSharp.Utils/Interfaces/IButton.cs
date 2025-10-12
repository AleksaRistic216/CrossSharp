using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IButton
    : IControl,
        IClickable,
        IBackgroundColorProvider,
        IHighlightable,
        IRenderStyleProvider
{
    ButtonImagePlacement ImagePlacement { get; set; }
    EventHandler? OnImagePlacementChange { get; set; }
    IEfficientImage? Image { get; set; }
    EventHandler? OnImageChange { get; set; }
    Alignment TextAlignment { get; set; }
    EventHandler? OnTextAlignmentChange { get; set; }
    string? Text { get; set; }
    EventHandler? OnTextChange { get; set; }
    object? Tag { get; set; }
    EventHandler? OnTagChange { get; set; }
}
