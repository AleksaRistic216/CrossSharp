using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class DataGridFactory : IDataGridFactory
{
    public IDataGrid Create() => new DataGrid();
}
