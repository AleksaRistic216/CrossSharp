namespace CrossSharp.Utils.Interfaces;

public interface ITheme
{
    ColorRgba BackgroundColor { get; set; }
    ColorRgba ButtonBackgroundColor { get; set; }
    bool UseNativeTitleBar { get; set; }
    int RoundedCornersRadius { get; set; }
    int FormBorderWidth { get; set; }
    ColorRgba FormBorderColor { get; set; }
    ColorRgba TitleBarBackgroundColor { get; set; }
    ColorRgba TitleBarForegroundColor { get; set; }
}
