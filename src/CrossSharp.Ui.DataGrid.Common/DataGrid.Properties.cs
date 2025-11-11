using System.Reflection;
using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class DataGrid
{
    Dictionary<string, PropertyInfo>? _dataSourceProperties;
    int _rowHeight = 0;
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            OnBackgroundColorChanged();
        }
    }
    IQueryable<IDataGridDataItem>? _dataSource;
    public IQueryable<IDataGridDataItem>? DataSource
    {
        get => _dataSource;
        set
        {
            if (_dataSource == value)
                return;
            _dataSource = value;
            OnDataSourceChanged();
        }
    }
    public IDataGridConfiguration Configuration { get; } = new DataGridConfiguration();
}
