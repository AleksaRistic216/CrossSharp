namespace CrossSharp.Utils.Interfaces;

public interface IDataGrid : IControl, IBackgroundColorProvider, IScrollable, IDockable
// , IRoundedCorners // cant at the moment, have problem with clipping
{
    IQueryable<IDataGridDataItem>? DataSource { get; set; }
    EventHandler? DataSourceChanged { get; set; }
    IDataGridConfiguration Configuration { get; }
    int CurrentlyLoadedItemsCount { get; }

    /// <summary>
    /// Gets or sets the number of rows to cache for smoother scrolling.
    /// If null, a default value will be used which is total renderable rows within the current viewport * 3.
    /// If you want to disable caching, set this to -1.
    /// </summary>
    int? RowsCacheCount { get; set; }
}
