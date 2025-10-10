using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ILabel : IControl, IForegroundColorProvider
{
    EventHandler<EventArgs>? OnTextChanged { get; set; }
    string Text { get; set; }
    FontFamily FontFamily { get; set; }
    int FontSize { get; set; }
}
