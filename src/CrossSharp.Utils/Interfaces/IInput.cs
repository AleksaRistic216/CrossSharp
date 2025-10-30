namespace CrossSharp.Utils.Interfaces;

public interface IInput : IControl, IClickable, IBackgroundColorProvider, IFocusable, IDockable
{
    bool MultiLine { get; set; }
    string Text { get; set; }
    EventHandler? OnTextChanged { get; set; }
    int FontSize { get; set; }
}
