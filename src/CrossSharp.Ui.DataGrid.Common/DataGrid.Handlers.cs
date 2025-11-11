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
}
