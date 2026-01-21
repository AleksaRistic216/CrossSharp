using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class DataGridFactory : IDataGridFactory
{
    public IDataGrid Create() => new DataGrid();
}
