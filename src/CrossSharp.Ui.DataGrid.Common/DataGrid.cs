using System.Drawing;
using System.Reflection;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class DataGrid : ControlBase, IDataGrid
{
    internal DataGrid()
    {
        _inputHandler.MouseWheel += InputHandlerOnMouseWheel;
        _inputHandler.KeyPressed += InputHandlerOnKeyPressed;
    }

    public override void Dispose()
    {
        _inputHandler.MouseWheel -= InputHandlerOnMouseWheel;
        _inputHandler.KeyPressed -= InputHandlerOnKeyPressed;
        base.Dispose();
    }

    public override void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor.Darkened;
        BorderWidth = 1;
        BorderColor = BackgroundColor.Darkened;
    }

    public override void Invalidate()
    {
        this.PerformDocking();
        _rowHeight = Services.GetSingleton<ITheme>().DefaultFontSize + 5;
        _itemsToLoad = RowsCacheCount switch
        {
            -1 => DataSource?.Count() ?? 0,
            0 => Height / _rowHeight * 3,
            null => Height / _rowHeight * 3,
            _ => RowsCacheCount.Value,
        };
        var itemsCount = DataSource?.Count() ?? 0;
        ContentBounds = new Rectangle(
            0,
            0,
            Width,
            (itemsCount + 1) * _rowHeight + _rowHeight /* header */
        );
        Viewport = new Rectangle(0, 0, Width, Height);
        CacheItems();
    }

    void InvalidateDataSourcePropertiesCache()
    {
        _dataSourceProperties = null;
        if (DataSource is null)
            return;
        var properties = DataSource.ElementType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        _dataSourceProperties = properties.ToDictionary(p => p.Name, p => p);
    }

    public override void DrawContent(ref IGraphics g)
    {
        if (DataSource is null)
            return;

        DrawHeader(ref g);
        DrawItems(ref g);
        ScrollableHelpers.DrawScrollBar(ref g, this);
    }

    void CacheItems()
    {
        if (_itemsToLoad <= 0)
            return;
        var skip = Math.Max(Viewport.Y / _rowHeight - _itemsToLoad / 3, 0);
        var take = _itemsToLoad;
        if (_selectedCells.Count > 0)
        {
            var firstSelectedRowIndex = _selectedCells.Min(c => c.RowIndex);
            var lastSelectedRowIndex = _selectedCells.Max(c => c.RowIndex);
            if (firstSelectedRowIndex < skip)
                skip = firstSelectedRowIndex;
            if (lastSelectedRowIndex >= skip + take)
                take = lastSelectedRowIndex - skip + 1;
        }
        if (_cachedItems is not null && _firstCachedItemIndex == skip && _cachedItems.Count == take)
            return;
        _firstCachedItemIndex = skip;
        _cachedItems = DataSource!.Skip(skip).Take(take).ToList();
    }

    void DrawHeader(ref IGraphics g)
    {
        if (_dataSourceProperties is null)
            return;
        var x = 0;
        foreach (KeyValuePair<string, PropertyInfo> dataSourceProperty in _dataSourceProperties)
        {
            var columnConfig = Configuration.Columns[dataSourceProperty.Key];
            var text = string.IsNullOrEmpty(columnConfig.HeaderText) ? dataSourceProperty.Key : columnConfig.HeaderText;
            DrawHeaderCell(
                ref g,
                x,
                0,
                columnConfig.Width,
                _rowHeight,
                text,
                columnConfig.HeaderBackgroundColor,
                columnConfig.HeaderTextColor
            );
            x += columnConfig.Width;
        }
    }

    void DrawItems(ref IGraphics g)
    {
        if (_dataSourceProperties is null)
            return;
        if (DataSource is null)
            return;
        if (_cachedItems is null)
            return;

        var skip = Viewport.Y / _rowHeight - _firstCachedItemIndex;
        var items = _cachedItems.Skip(skip).Take(Viewport.Height / _rowHeight + 1).ToList();
        var y = _rowHeight; // This is header height
        for (int i = 0; i < items.Count; i++)
        {
            var rowIndex = _firstCachedItemIndex + skip + i;
            var x = 0;
            var item = items[i];
            foreach (KeyValuePair<string, PropertyInfo> dataSourceProperty in _dataSourceProperties)
            {
                var columnConfig = Configuration.Columns[dataSourceProperty.Key];
                var cellValue = dataSourceProperty.Value.GetValue(item);
                var text = cellValue?.ToString() ?? string.Empty;
                DrawCell(
                    ref g,
                    x,
                    y,
                    columnConfig.Width,
                    Services.GetSingleton<ITheme>().DefaultFontSize + 15,
                    text,
                    ColorRgba.White,
                    ColorRgba.Black,
                    dataSourceProperty.Key,
                    rowIndex
                );
                x += columnConfig.Width;
            }
            y += _rowHeight;
        }
    }

    void DrawHeaderCell(
        ref IGraphics g,
        int x,
        int y,
        int width,
        int height,
        string text,
        ColorRgba backgroundColor,
        ColorRgba textColor
    ) => DrawCell(ref g, x, y, width, height, text, backgroundColor, textColor, string.Empty, -1);

    void DrawCell(
        ref IGraphics g,
        int x,
        int y,
        int width,
        int height,
        string text,
        ColorRgba backgroundColor,
        ColorRgba textColor,
        string column,
        int actualRowIndexWithinDataSource
    )
    {
        if (IsCellSelected(column, actualRowIndexWithinDataSource))
        {
            backgroundColor = backgroundColor.Darkened;
            textColor = backgroundColor.Contrasted;
        }
        var clipState = g.GetClipState();
        g.SetClip(ClipState.Create(clipState, clipState.Bounds, 0));
        g.FillRectangle(x, y, width, height, backgroundColor);
        g.DrawText(
            text,
            x + 2,
            y + 2,
            Services.GetSingleton<ITheme>().DefaultFontFamily,
            Services.GetSingleton<ITheme>().DefaultFontSize,
            textColor
        );
        // Need to update clip state because for some reason when I try drawing rectangle with no rounded corners
        // but current state has rounded corners, it doesn't draw it at all.
        g.DrawRectangle(x, y, width, height, BackgroundColor.Darkened, BorderWidth, 0);
        g.SetClip(clipState);
    }

    bool IsCellSelected(string column, int rowIndex)
    {
        // Here should be if selection mode whole row then just check row index
        return _selectedCells.Any(c => c.Column == column && c.RowIndex == rowIndex);
    }

    string GetNextColumnName(string lastCellColumn, int nextColumnsCount)
    {
        if (_dataSourceProperties is null)
            throw new InvalidOperationException("Data source properties are not initialized.");
        var keys = _dataSourceProperties.Keys.ToList();
        var currentIndex = keys.IndexOf(lastCellColumn);
        var targetIndex = currentIndex + nextColumnsCount;
        if (currentIndex == -1 || targetIndex >= keys.Count || targetIndex < 0)
            return lastCellColumn; // No next column, return the same
        return keys[targetIndex];
    }
}
