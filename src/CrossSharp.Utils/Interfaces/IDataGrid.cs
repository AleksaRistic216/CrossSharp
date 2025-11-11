namespace CrossSharp.Utils.Interfaces;

public interface IDataGrid : IControl, IBackgroundColorProvider
// , IRoundedCorners // cant at the moment, have problem with clipping
{
    IQueryable<IDataGridDataItem>? DataSource { get; set; }
    EventHandler? DataSourceChanged { get; set; }
    IDataGridConfiguration Configuration { get; }
}
