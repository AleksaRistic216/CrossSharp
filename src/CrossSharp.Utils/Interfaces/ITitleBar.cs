using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ITitleBar : IBoundsProvider, IBackgroundColorProvider, IForegroundColorProvider
{
    TitleBarType Type { get; set; }
    EventHandler? TypeChanged { get; set; }
    void Show();
    void Invalidate();
    void Redraw();
}
