namespace CrossSharp.Utils.Interfaces;

public interface IDataGrid : IControl, IBackgroundColorProvider, IRoundedCorners
{
    IQueryable<IDataGridDataItem>? DataSource { get; set; }
    EventHandler? DataSourceChanged { get; set; }
    IDataGridConfiguration Configuration { get; }
}
