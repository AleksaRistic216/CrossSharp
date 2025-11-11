using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class DataGrid() : CrossControl<IDataGrid>(Services.GetSingleton<IDataGridFactory>().Create()), IDataGrid
{
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }
    public IQueryable<IDataGridDataItem>? DataSource
    {
        get => Implementation.DataSource;
        set => Implementation.DataSource = value;
    }
    public EventHandler? DataSourceChanged
    {
        get => Implementation.DataSourceChanged;
        set => Implementation.DataSourceChanged = value;
    }
    public IDataGridConfiguration Configuration => Implementation.Configuration;
}
