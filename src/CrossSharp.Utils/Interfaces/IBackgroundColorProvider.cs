namespace CrossSharp.Utils.Interfaces;

public interface IBackgroundColorProvider
{
    ColorRgba BackgroundColor { get; set; }
    EventHandler? OnBackgroundColorChange { get; set; }
}
