using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class DataGridFactory : IDataGridFactory
{
    public IDataGrid Create() => new DataGrid();
}
