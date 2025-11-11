using System.Reflection;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class DataGrid : ControlBase, IDataGrid
{
    public override void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor.Darkened;
        BorderWidth = 1;
        BorderColor = BackgroundColor.Darkened;
    }

    public override void Invalidate()
    {
        _rowHeight = Services.GetSingleton<ITheme>().DefaultFontSize + 15;
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
            DrawCell(
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

        var itemsCount = 5;
        var items = DataSource!.Take(itemsCount).ToList();
        var y = _rowHeight; // This is header height
        for (int i = 0; i < items.Count; i++)
        {
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
                    ColorRgba.Black
                );
                x += columnConfig.Width;
            }
            y += _rowHeight;
        }
    }

    void DrawCell(
        ref IGraphics g,
        int x,
        int y,
        int width,
        int height,
        string text,
        ColorRgba backgroundColor,
        ColorRgba textColor
    )
    {
        var clipState = g.GetClipState();
        g.SetClip(ClipState.Create(clipState, clipState.Bounds, 0));
        g.FillRectangle(x, y, width, height, backgroundColor);
        g.DrawText(
            text,
            x + 5,
            y + 5,
            Services.GetSingleton<ITheme>().DefaultFontFamily,
            Services.GetSingleton<ITheme>().DefaultFontSize,
            textColor
        );
        // Need to update clip state because for some reason when I try drawing rectangle with no rounded corners
        // but current state has rounded corners, it doesn't draw it at all.
        g.DrawRectangle(x, y, width, height, BackgroundColor.Darkened, BorderWidth, 0);
        g.SetClip(clipState);
    }
}
