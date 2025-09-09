using System.Drawing;
namespace CrossSharp.Utils.Interfaces;

public interface ISizeProvider {
    int Width { get; set; }
    int Height { get; set; }
    EventHandler<Size>? OnSizeChanged { get; set; }
}