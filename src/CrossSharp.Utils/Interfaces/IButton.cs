namespace CrossSharp.Utils.Interfaces;

public interface IButton : IControl, IClickable, IBackgroundColorProvider
{
    string Text { get; set; }
    EventHandler? OnTextChange { get; set; }
}
