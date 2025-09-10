using CrossSharp.Utils.Enums;
namespace CrossSharp.Utils.Interfaces;

public interface ITitleBar : IControl {
    TitleBarType Type { get; set; }
    EventHandler? TypeChanged { get; set; }
}