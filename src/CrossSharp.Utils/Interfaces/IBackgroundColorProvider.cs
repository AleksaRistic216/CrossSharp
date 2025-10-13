namespace CrossSharp.Utils.Interfaces;

public interface IBackgroundColorProvider
{
    ColorRgba BackgroundColor { get; set; }
    EventHandler? BackgroundColorChanged { get; set; }
}
