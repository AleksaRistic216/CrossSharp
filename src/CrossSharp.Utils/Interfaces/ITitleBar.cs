using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ITitleBar : IBoundsProvider
{
    TitleBarType Type { get; set; }
    EventHandler? TypeChanged { get; set; }
    void Show();
    void Invalidate();
}
