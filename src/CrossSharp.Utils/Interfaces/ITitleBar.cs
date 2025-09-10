using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ITitleBar
{
    TitleBarType Type { get; set; }
    EventHandler? TypeChanged { get; set; }
    int Height { get; set; }
    void Show();
}
