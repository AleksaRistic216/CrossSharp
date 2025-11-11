using System.Drawing;
using System.Reflection;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class DataGrid
{
    Dictionary<string, PropertyInfo>? _dataSourceProperties;
    int _rowHeight;
    int _itemsToLoad;
    readonly IInputHandler _inputHandler = Services.GetSingleton<IInputHandler>();
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

    int _firstCachedItemIndex;

    /// <summary>
    /// Items loaded into memory cache. Usually more than viewport to allow smooth scrolling.
    /// </summary>
    List<IDataGridDataItem>? _cachedItems;
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
    public ScrollableMode Scrollable { get; set; } = ScrollableMode.Vertical;
    Rectangle _viewport = Rectangle.Empty;
    public Rectangle Viewport
    {
        get => _viewport;
        set => _viewport = value;
    }
    public Rectangle ContentBounds { get; set; } = Rectangle.Empty;
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
}
