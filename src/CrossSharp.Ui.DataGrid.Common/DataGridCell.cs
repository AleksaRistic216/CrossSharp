using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

class DataGridCell(int rowIndex, string column) : IDataGridCell
{
    public int RowIndex { get; } = rowIndex;
    public string Column { get; } = column;
}
