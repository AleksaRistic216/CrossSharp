using System.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface ISizeProvider
{
    int Width { get; set; }
    int Height { get; set; }
    Size Size => new(Width, Height);
    EventHandler<Size>? OnSizeChanged { get; set; }
}
