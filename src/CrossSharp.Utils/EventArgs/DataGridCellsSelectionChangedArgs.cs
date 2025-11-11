using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.EventArgs;

public class DataGridCellsSelectionChangedArgs(IDataGridCell[] selectedCells) : System.EventArgs
{
    public IDataGridCell[] SelectedCells { get; private set; } = selectedCells;
}
