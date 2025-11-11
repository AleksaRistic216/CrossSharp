using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

partial class DataGrid
{
    public EventHandler? BackgroundColorChanged { get; set; }

    void RaiseBackgroundColorChanged()
    {
        BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnBackgroundColorChanged()
    {
        Invalidate();
    }

    public EventHandler? DataSourceChanged { get; set; }

    void RaiseDataSourceChanged()
    {
        DataSourceChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnDataSourceChanged()
    {
        InvalidateDataSourcePropertiesCache();
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    void InputHandlerOnMouseWheel(object? sender, MouseWheelInputArgs e)
    {
        if (!IsMouseOver)
            return;

        var rotation = e.Rotation;
        rotation /= 10;
        if (Math.Abs(rotation) <= 0)
            return;
        if (Scrollable == ScrollableMode.Vertical)
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, this, ref _viewport);
        else if (Scrollable == ScrollableMode.Horizontal)
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, this, ref _viewport);
        else if (Scrollable == ScrollableMode.Both)
        {
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, this, ref _viewport);
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, this, ref _viewport);
        }
        OnScrolled();
    }

    public EventHandler? Scrolled { get; set; }

    void RaiseScrolled()
    {
        Scrolled?.Invoke(this, EventArgs.Empty);
    }

    void OnScrolled()
    {
        CacheItems();
        RaiseScrolled();
    }
}
