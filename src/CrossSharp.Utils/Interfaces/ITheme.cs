namespace CrossSharp.Utils.Interfaces;

public interface ITheme
{
    ColorRgba BackgroundColor { get; set; }
    ColorRgba ButtonBackgroundColor { get; set; }
    bool UseNativeTitleBar { get; set; }
    int RoundedCornersRadius { get; set; }
}
