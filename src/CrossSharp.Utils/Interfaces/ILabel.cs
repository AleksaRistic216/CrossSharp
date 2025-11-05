using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ILabel : IControl, IForegroundColorProvider, IAccordionItem
{
    EventHandler<System.EventArgs>? OnTextChanged { get; set; }
    string Text { get; set; }
    FontFamily FontFamily { get; set; }
    int FontSize { get; set; }
}
