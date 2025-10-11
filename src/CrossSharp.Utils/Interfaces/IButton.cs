namespace CrossSharp.Utils.Interfaces;

public interface IButton : IControl, IClickable, IBackgroundColorProvider, IHighlightable
{
    string Text { get; set; }
    EventHandler? OnTextChange { get; set; }
}
