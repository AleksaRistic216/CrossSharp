namespace CrossSharp.Utils.Interfaces;

public interface IInput
    : IControl,
        IClickable,
        IBackgroundColorProvider,
        IFocusable,
        IDockable,
        IRoundedCorners
{
    string? Placeholder { get; set; }
    EventHandler? PlaceholderChanged { get; set; }
    bool MultiLine { get; set; }
    string Text { get; set; }
    EventHandler? TextChanged { get; set; }
    int FontSize { get; set; }
}
