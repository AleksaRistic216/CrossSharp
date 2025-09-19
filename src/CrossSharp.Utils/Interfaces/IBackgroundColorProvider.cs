using System.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface IBackgroundColorProvider
{
    ColorRgba BackgroundColor { get; set; }
}
