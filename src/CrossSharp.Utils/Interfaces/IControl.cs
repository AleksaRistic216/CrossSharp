namespace CrossSharp.Utils.Interfaces;

public interface IControl
    : IDisposable,
        IBorder,
        ILocationProvider,
        ISizeProvider,
        IChild,
        IIsMouseOverProvider,
        IMarginProvider
{
    void PerformTheme();
    EventHandler? ThemePerformed { get; set; }

    /// <summary>
    /// Invoked before performing the theming process.
    /// If this function returns true, default theming process will be skipped.
    /// </summary>
    bool Visible { get; set; }
    void Invalidate();
    EventHandler? Invalidated { get; set; }
    void Draw(ref IGraphics graphics);
    EventHandler? Disposing { get; set; }

    /// <summary>
    /// Gets or sets the index of the control within its parent container.
    /// This determines the order in which controls are rendered and laid out.
    /// </summary>
    int Index { get; set; }
}
