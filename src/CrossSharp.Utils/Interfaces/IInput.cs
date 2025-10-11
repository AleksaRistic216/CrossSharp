namespace CrossSharp.Utils.Interfaces;

public interface IInput : IControl, IClickable, IBackgroundColorProvider
{
    string Text { get; set; }
    EventHandler? OnTextChanged { get; set; }
    int FontSize { get; }
}
