namespace CrossSharp.Utils.Interfaces;

public interface ILabel : IGtkWidget, ICenterPanelChild, IForegroundColorProvider
{
    EventHandler<EventArgs>? OnTextChanged { get; set; }
    string Text { get; set; }
    string FontFamily { get; set; }
    int FontSize { get; set; }
}
