using System.Collections;

namespace CrossSharp.Utils.Interfaces;

public interface IWindowContainer
    : IDisposable,
        IRelativeHandle,
        IBackgroundColorProvider,
        ISizeProvider,
        IBorder,
        IClipLimiter,
        ICollection
{
    List<IControl> Items { get; }
    void Show();
    void Add(IControl control);
    void Redraw();
    void PerformLayout();

    /// <summary>
    /// Attach a control to this container
    /// </summary>
    /// <param name="control"></param>
    void Attach(IControl control);

    /// <summary>
    /// Attach a control to this container at specified position and size
    /// </summary>
    /// <param name="control"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="width">The number of columns that child will span.</param>
    /// <param name="height">The number of rows that child will span.</param>
    void Attach(IControl control, int column, int row, int width, int height);
}
