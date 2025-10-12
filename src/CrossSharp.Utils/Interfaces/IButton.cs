using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IButton
    : IControl,
        IClickable,
        IBackgroundColorProvider,
        IHighlightable,
        IRenderStyleProvider
{
    Alignment TextAlignment { get; set; }
    EventHandler? OnTextAlignmentChange { get; set; }
    string Text { get; set; }
    EventHandler? OnTextChange { get; set; }
    object? Tag { get; set; }
    EventHandler? OnTagChange { get; set; }
}
